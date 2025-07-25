using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MediatR;
using Newtonsoft.Json;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Dtos.Ubl;
using NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.Framework.Core.Persistence;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Invoice;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Specifications;
using Microsoft.Extensions.Logging;
using NexKoala.WebApi.Invoice.Application.Features.UomMappings.Specifications;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Specifications;
using NexKoala.WebApi.Invoice.Domain.Events;
using NexKoala.WebApi.Invoice.Application.Features.Classifications.Specifications;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.SubmitInvoice.v1;

public sealed class SubmitInvoiceComamndHandler
    : IRequestHandler<SubmitInvoiceCommand, object>
{
    private readonly ILhdnApi _lhdnApi;
    private readonly IRepository<InvoiceDocument> _invoiceDocumentRepository;
    private readonly IRepository<UomMapping> _uomMappingRepository;
    private readonly IRepository<Partner> _partnerRepository;
    private readonly IRepository<ClassificationMapping> _classificationMappingRepository;
    private readonly ILogger<SubmitInvoiceComamndHandler> _logger;
    private readonly IPublisher _publisher;
    private readonly IMsicService _msicService;

    public SubmitInvoiceComamndHandler(ILhdnApi lhdnApi, [FromKeyedServices("invoice:invoiceDocuments")] IRepository<InvoiceDocument> invoiceDocumentRepository,
        [FromKeyedServices("invoice:uomMappings")] IRepository<UomMapping> uomMappingRepository, [FromKeyedServices("invoice:partners")] IRepository<Partner> partnerRepository,
        [FromKeyedServices("invoice:classificationMappings")] IRepository<ClassificationMapping> classificationMappingRepository,
        ILogger<SubmitInvoiceComamndHandler> logger, IPublisher publisher, IMsicService msicService)
    {
        _lhdnApi = lhdnApi;
        _invoiceDocumentRepository = invoiceDocumentRepository;
        _uomMappingRepository = uomMappingRepository;
        _partnerRepository = partnerRepository;
        _classificationMappingRepository = classificationMappingRepository;
        _logger = logger;
        _publisher = publisher;
        _msicService = msicService;
    }

    public async Task<object> Handle(
        SubmitInvoiceCommand request,
        CancellationToken cancellationToken
    )
    {
        var now = DateTime.UtcNow.AddSeconds(-10);

        if (!Guid.TryParse(request.UserId, out Guid userId))
        {
            return new Response<object>("Invalid or missing user ID");
        }

        // get user TIN
        var partner = await _partnerRepository.FirstOrDefaultAsync(new PartnerByUserIdSpec(request.UserId), cancellationToken);
        string partnerTin = partner!.Tin;

        if (string.IsNullOrWhiteSpace(partnerTin))
        {
            return new Response<object>($"The TIN (Taxpayer Identification Number) for {partner.Name} is not set.");
        }

        // check submission is exist and submitted successfully
        var invoiceIrns = request.Invoices.Select(x => x.Irn).ToList();
        var existingInvoices = await _invoiceDocumentRepository
            .ListAsync(new GetInvoiceDocumentByInvoiceNumber(invoiceIrns, true), cancellationToken);

        if (existingInvoices.Any())
        {
            var duplicateNumbers = string.Join(", ", existingInvoices.Select(x => x.InvoiceNumber));
            _logger.LogError($"Invoices already submitted: {duplicateNumbers}");
            return new Response<object>($"Invoices already submitted: {duplicateNumbers}");
        }

        // get user uom mapping
        var userRequestUomCodes = request.Invoices
            .SelectMany(invoice => invoice.ItemList)
            .Select(item => item.Unit)
            .Where(unit => !string.IsNullOrWhiteSpace(unit))
            .Distinct()
            .ToList();

        var userUomMappings = await _uomMappingRepository
            .ListAsync(new UomMappingByUserIdAndCodeList(userId, userRequestUomCodes), cancellationToken);

        var mappedCodes = userUomMappings
            .Select(m => m.Uom.Code)
            .Distinct()
            .ToList();

        // find missing codes
        var missingCodes = userRequestUomCodes
            .Except(mappedCodes, StringComparer.OrdinalIgnoreCase)
            .ToList();

        // check if any are missing
        if (missingCodes.Count != 0)
        {
            _logger.LogError($"User with ID {userId} is missing UOM mappings for: {string.Join(", ", missingCodes)}");
            return new Response<object>($"Missing UOM mapping for: {string.Join(", ", missingCodes)}");
        }

        var uomMappingDictionary = userUomMappings.ToDictionary(
            m => m.Uom.Code, 
            m => m.LhdnUomCode,
            StringComparer.OrdinalIgnoreCase
        );

        // get user classification mapping
        var userRequestClassificationCodes = request.Invoices
            .SelectMany(invoice => invoice.ItemList)
            .Select(item => item.ClassificationCode)
            .Where(classification => !string.IsNullOrWhiteSpace(classification))
            .Distinct()
            .ToList();

        var userClassificationMappings = await _classificationMappingRepository
            .ListAsync(new ClassificationMappingWithClassification(userId, userRequestClassificationCodes), cancellationToken);

        var mappedClassificationCodes = userClassificationMappings
            .Select(m => m.Classification.Code)
            .Distinct()
            .ToList();

        // find missing codes
        var missingClassificationCodes = userRequestClassificationCodes
            .Except(mappedClassificationCodes, StringComparer.OrdinalIgnoreCase)
            .ToList();

        // check if any are missing
        if (missingClassificationCodes.Count != 0)
        {
            _logger.LogError($"User with ID {userId} is missing classification mappings for: {string.Join(", ", missingClassificationCodes)}");
            return new Response<object>($"Missing classification mapping for: {string.Join(", ", missingClassificationCodes)}");
        }

        var classificationMappingDictionary = userClassificationMappings.ToDictionary(
            m => m.Classification.Code,
            m => m.LhdnClassificationCode,
            StringComparer.OrdinalIgnoreCase
        );

        var documentList = new List<UblDocument>();
        var invoiceDocuments = new List<InvoiceDocument>();
        foreach (var invoice in request.Invoices)
        {
            // if tax amount is 0, sum the tax amount of item in itemlist
            if (invoice.TaxAmount <= 0)
            {
                decimal taxAmt = 0;
                foreach(var item in invoice.ItemList)
                {
                    taxAmt += item.TaxAmount;
                }

                invoice.TaxAmount = taxAmt;

                await _publisher.Publish(new InvoiceAuditPublishedEvent(new()
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Operation = "Tax Amount Update From Items",
                        Entity = "InvoiceDocument",
                        UserId = userId,
                        DateTime = DateTime.UtcNow,
                    }
                }));
            }

            var supplierIdType = IsValidIdType(invoice.SupplierIdType) ? invoice.SupplierIdType : "NRIC";
            var supplierSst = !string.IsNullOrWhiteSpace(invoice.SupplierSST) ? invoice.SupplierSST : "NA";

            var customerIdType = IsValidIdType(invoice.CustomerIdType) ? invoice.CustomerIdType : "NRIC";
            var customerSst = !string.IsNullOrWhiteSpace(invoice.CustomerSST) ? invoice.CustomerSST : "NA";

            // Step 1: Construct the Invoice JSON document
            var ublInvoice = new UblInvoiceDocument()
            {
                _D = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2",
                _A = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2",
                _B = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2",
                Invoice =
                [
                    new UblInvoice()
                    {
                        Id = [new() { _ = invoice.Irn }],
                        IssueDate = [new() { _ = now.ToString("yyyy-MM-dd") }],
                        IssueTime = [new() { _ = now.ToString("HH:mm:ss") + "Z" }],
                        InvoiceTypeCode =
                        [
                            new() { _ = invoice.InvoiceTypeCode, ListVersionId = "1.0" },
                        ],
                        DocumentCurrencyCode = [new() { _ = invoice.CurrencyCode }],
                        InvoicePeriod =
                        [
                            new()
                            {
                                StartDate = [new() { _ = invoice.StartDate }],
                                EndDate = [new() { _ = invoice.EndDate }],
                                Description = [new() { _ = invoice.InvoicePeriodDescription }],
                            },
                        ],
                        // need to declare if goods imported
                        BillingReference = new[]
                        {
                            new BillingReference
                            {
                                AdditionalDocumentReference = new[]
                                {
                                    new DocumentReference
                                    {
                                        Id = new[]
                                        {
                                            new BasicComponent { _ = invoice.BillingReferenceID ?? "" },
                                        },
                                        DocumentType = new[]
                                        {
                                            new BasicComponent { _ = "CustomsImportForm" },
                                        },
                                    },
                                },
                            },
                        },
                        AdditionalDocumentReference = new[]
                        {
                            new DocumentReference
                            {
                                Id = new[]
                                {
                                    new BasicComponent
                                    {
                                        _ = invoice.AdditionalDocumentReferenceID ?? "",
                                    },
                                },
                                DocumentType = new[] { new BasicComponent { _ = "CustomsImportForm" } },
                            },
                        },
                        AccountingSupplierParty =
                        [
                            new()
                            {
                                AdditionalAccountId =
                                [
                                    new()
                                    {
                                        _ = invoice.SupplierAdditionalAccountID ?? "",
                                        SchemeAgencyName = "CertEx",
                                    },
                                ],
                                Party =
                                [
                                    new()
                                    {
                                        IndustryClassificationCode =
                                        [
                                            new()
                                            {
                                                _ = invoice.SupplierIndustryCode,
                                                Name = _msicService.GetDescription(invoice.SupplierIndustryCode),
                                            },
                                        ],
                                        PartyIdentification =
                                        [
                                            new()
                                            {
                                                Id =
                                                [
                                                    new() { _ = invoice.SupplierTIN, SchemeId = "TIN" },
                                                ],
                                            },
                                            new()
                                            {
                                                Id =
                                                [
                                                    new()
                                                    {
                                                        _ = invoice.SupplierBRN,
                                                        SchemeId = supplierIdType,
                                                    },
                                                ],
                                            },
                                            new()
                                            {
                                                Id =
                                                [
                                                    new() { _ = supplierSst, SchemeId = "SST" },
                                                ],
                                            },
                                            //new()
                                            //{
                                            //    Id =
                                            //    [
                                            //        new() { _ = "", SchemeId = "TTX" },
                                            //    ],
                                            //},
                                        ],
                                        PostalAddress =
                                        [
                                            new()
                                            {
                                                CityName = [new() { _ = invoice.SupplierCity }],
                                                PostalZone = [new() { _ = invoice.SupplierPostalCode }],
                                                CountrySubentityCode =
                                                [
                                                    new() { _ = invoice.SupplierCountrySubentityCode },
                                                ],
                                                AddressLine =
                                                [
                                                    new()
                                                    {
                                                        Line =
                                                        [
                                                            new() { _ = invoice.SupplierAddressLine1 },
                                                        ],
                                                    },
                                                    new()
                                                    {
                                                        Line =
                                                        [
                                                            new() { _ = invoice.SupplierAddressLine2 },
                                                        ],
                                                    },
                                                    new()
                                                    {
                                                        Line =
                                                        [
                                                            new() { _ = invoice.SupplierAddressLine3 },
                                                        ],
                                                    },
                                                ],
                                                Country =
                                                [
                                                    new()
                                                    {
                                                        IdentificationCode =
                                                        [
                                                            new()
                                                            {
                                                                _ = invoice.SupplierCountryCode,
                                                                ListId = "3166-1",
                                                                ListAgencyId = "ISO",
                                                            },
                                                        ],
                                                    },
                                                ],
                                            },
                                        ],
                                        PartyLegalEntity =
                                        [
                                            new()
                                            {
                                                RegistrationName = [new() { _ = invoice.SupplierName }],
                                            },
                                        ],
                                        Contact =
                                        [
                                            new()
                                            {
                                                Telephone = [new() { _ = invoice.SupplierTelephone }],
                                                ElectronicMail = [new() { _ = invoice.SupplierEmail }],
                                            },
                                        ],
                                    },
                                ],
                            },
                        ],
                        AccountingCustomerParty =
                        [
                            new()
                            {
                                Party =
                                [
                                    new()
                                    {
                                        PartyIdentification =
                                        [
                                            new()
                                            {
                                                Id =
                                                [
                                                    new() { _ = invoice.CustomerTIN, SchemeId = "TIN" },
                                                ],
                                            },
                                            new()
                                            {
                                                Id =
                                                [
                                                    new()
                                                    {
                                                        _ = invoice.CustomerBRN ?? "",
                                                        SchemeId = customerIdType,
                                                    },
                                                ],
                                            },
                                            new()
                                            {
                                                Id =
                                                [
                                                    new()
                                                    {
                                                        _ = customerSst,
                                                        SchemeId = "SST",
                                                    },
                                                ],
                                            },
                                        ],
                                        PostalAddress =
                                        [
                                            new()
                                            {
                                                CityName = [new() { _ = invoice.CustomerCity }],
                                                PostalZone = [new() { _ = invoice.CustomerPostalCode }],
                                                CountrySubentityCode =
                                                [
                                                    new() { _ = invoice.CustomerCountrySubentityCode },
                                                ],
                                                AddressLine =
                                                [
                                                    new()
                                                    {
                                                        Line =
                                                        [
                                                            new() { _ = invoice.CustomerAddressLine1 },
                                                        ],
                                                    },
                                                    new()
                                                    {
                                                        Line =
                                                        [
                                                            new() { _ = invoice.CustomerAddressLine2 },
                                                        ],
                                                    },
                                                    new()
                                                    {
                                                        Line =
                                                        [
                                                            new() { _ = invoice.CustomerAddressLine3 },
                                                        ],
                                                    },
                                                ],
                                                Country =
                                                [
                                                    new()
                                                    {
                                                        IdentificationCode =
                                                        [
                                                            new()
                                                            {
                                                                _ = invoice.CustomerCountryCode,
                                                                ListId = "ISO3166-1",
                                                                ListAgencyId = "6",
                                                            },
                                                        ],
                                                    },
                                                ],
                                            },
                                        ],
                                        PartyLegalEntity =
                                        [
                                            new()
                                            {
                                                RegistrationName = [new() { _ = invoice.CustomerName }],
                                            },
                                        ],
                                        Contact =
                                        [
                                            new()
                                            {
                                                Telephone = [new() { _ = FormatPhoneNumber(invoice.CustomerTelephone) }],
                                                ElectronicMail = [new() { _ = invoice.CustomerEmail }],
                                            },
                                        ],
                                    },
                                ],
                            },
                        ],
                        LegalMonetaryTotal =
                        [
                            new()
                            {
                                PayableAmount =
                                [
                                    new()
                                    {
                                        _ = invoice.TotalPayableAmount,
                                        CurrencyId = invoice.CurrencyCode,
                                    },
                                ],
                                TaxExclusiveAmount =
                                [
                                    new() { _ = invoice.TotalExcludingTax, CurrencyId = invoice.CurrencyCode },
                                ],
                                TaxInclusiveAmount =
                                [
                                    new() { _ = invoice.TotalIncludingTax, CurrencyId = invoice.CurrencyCode },
                                ],
                            },
                        ],
                        InvoiceLine =
                        [
                            .. invoice.ItemList.ConvertAll(item => new Dtos.Ubl.Invoice.InvoiceLine()
                            {
                                Id = [new() { _ = item.Id.ToString() }],
                                InvoicedQuantity = [new() { _ = item.Qty, UnitCode = uomMappingDictionary[item.Unit] }],
                                LineExtensionAmount =
                                [
                                    new() { _ = item.Subtotal, CurrencyId = invoice.CurrencyCode },
                                ],
                                Item =
                                [
                                    new()
                                    {
                                        Description = [new() { _ = item.Description }],

                                        // Add CommodityClassification section
                                        CommodityClassification =
                                        [
                                            new()
                                            {
                                                ItemClassificationCode =
                                                [
                                                    new() { _ = classificationMappingDictionary[item.ClassificationCode], ListId = "CLASS" },
                                                ],
                                            },
                                        ],

                                        // Add OriginCountry section
                                        OriginCountry =
                                        [
                                            new()
                                            {
                                                IdentificationCode =
                                                [
                                                    new()
                                                    {
                                                        _ = "MYS", // Replace with appropriate ISO 3166-1 code for the country
                                                        ListId = "ISO3166-1",
                                                        ListAgencyId = "6",
                                                    },
                                                ],
                                            },
                                        ],
                                    },
                                ],
                                Price =
                                [
                                    new()
                                    {
                                        PriceAmount =
                                        [
                                            new()
                                            {
                                                _ = item.UnitPrice,
                                                CurrencyId = invoice.CurrencyCode,
                                            },
                                        ],
                                    },
                                ],
                                TaxTotal =
                                [
                                    new()
                                    {
                                        TaxAmount =
                                        [
                                            new()
                                            {
                                                _ = item.TaxAmount,
                                                CurrencyId = invoice.CurrencyCode,
                                            },
                                        ],
                                        TaxSubtotal =
                                        [
                                            new()
                                            {
                                                TaxableAmount =
                                                [
                                                    new()
                                                    {
                                                        _ = item.TaxableAmount,
                                                        CurrencyId = invoice.CurrencyCode,
                                                    },
                                                ],
                                                TaxAmount =
                                                [
                                                    new()
                                                    {
                                                        _ = item.TaxAmount,
                                                        CurrencyId = invoice.CurrencyCode,
                                                    },
                                                ],
                                                TaxCategory =
                                                [
                                                    new()
                                                    {
                                                        Id = [new() { _ = "01" }],
                                                        TaxScheme =
                                                        [
                                                            new()
                                                            {
                                                                Id =
                                                                [
                                                                    new()
                                                                    {
                                                                        _ = "OTH",
                                                                        SchemeId = "UN/ECE 5153",
                                                                        SchemeAgencyId = "6",
                                                                    },
                                                                ],
                                                            },
                                                        ],
                                                    },
                                                ],
                                            },
                                        ],
                                    },
                                ],
                                ItemPriceExtension =
                                [
                                    new()
                                    {
                                        Amount =
                                        [
                                            new()
                                            {
                                                _ = item.Subtotal,
                                                CurrencyId = invoice.CurrencyCode,
                                            }
                                        ]
                                    }
                                ]
                            }),
                        ],
                        TaxTotal =
                        [
                            new()
                            {
                                TaxAmount =
                                [
                                    new()
                                    {
                                        _ = Convert.ToDecimal(invoice.TaxAmount),
                                        CurrencyId = invoice.CurrencyCode,
                                    },
                                ],
                                TaxSubtotal =
                                [
                                    new()
                                    {
                                        TaxableAmount =
                                        [
                                            new()
                                            {
                                                _ = Convert.ToDecimal(invoice.TaxableAmount),
                                                CurrencyId = invoice.CurrencyCode,
                                            },
                                        ],
                                        TaxAmount =
                                        [
                                            new()
                                            {
                                                _ = Convert.ToDecimal(invoice.TaxAmount),
                                                CurrencyId = invoice.CurrencyCode,
                                            },
                                        ],
                                        TaxCategory =
                                        [
                                            new()
                                            {
                                                Id = [new() { _ = "01" }],
                                                TaxScheme =
                                                [
                                                    new()
                                                    {
                                                        Id =
                                                        [
                                                            new()
                                                            {
                                                                _ = "OTH",
                                                                SchemeId = "UN/ECE 5153",
                                                                SchemeAgencyId = "6",
                                                            },
                                                        ],
                                                    },
                                                ],
                                            },
                                        ],
                                    },
                                ],
                            },
                        ],
                        //UBLExtensions = new[]
                        //{
                        //    new
                        //    {
                        //        UBLExtension = new[]
                        //        {
                        //            new
                        //            {
                        //                ExtensionURI = new[] { new { _ = "urn:oasis:names:specification:ubl:dsig:enveloped:xades" } },
                        //                ExtensionContent = new[]
                        //                {
                        //                    new
                        //                    {
                        //                        UBLDocumentSignatures = new[]
                        //                        {
                        //                            newBRN
                        //                            {
                        //                                SignatureInformation = new[]
                        //                                {
                        //                                    new
                        //                                    {
                        //                                        ID = new[] { new { _ = "urn:oasis:names:specification:ubl:signature:1" } },
                        //                                        ReferencedSignatureID = new[] { new { _ = "urn:oasis:names:specification:ubl:signature:Invoice" } },
                        //                                        Signature = new[]
                        //                                        {
                        //                                            new
                        //                                            {
                        //                                                Id = "signature",
                        //                                                Object = new[]
                        //                                                {
                        //                                                    new
                        //                                                    {
                        //                                                        QualifyingProperties = new[]
                        //                                                        {
                        //                                                            new
                        //                                                            {
                        //                                                                Target = "signature",
                        //                                                                SignedProperties = new[]
                        //                                                                {
                        //                                                                    new
                        //                                                                    {
                        //                                                                        Id = "id-xades-signed-props",
                        //                                                                        SignedSignatureProperties = new[]
                        //                                                                        {
                        //                                                                            new
                        //                                                                            {
                        //                                                                                SigningTime = new[] { new { _ = "2024-07-27T13:22:39Z" } },
                        //                                                                                SigningCertificate = new[]
                        //                                                                                {
                        //                                                                                    new
                        //                                                                                    {
                        //                                                                                        Cert = new[]
                        //                                                                                        {
                        //                                                                                            new
                        //                                                                                            {
                        //                                                                                                CertDigest = new[]
                        //                                                                                                {
                        //                                                                                                    new
                        //                                                                                                    {
                        //                                                                                                        DigestMethod = new[]
                        //                                                                                                        {
                        //                                                                                                            new { _ = "", Algorithm = "http://www.w3.org/2001/04/xmlenc#sha256" }
                        //                                                                                                        },
                        //                                                                                                        DigestValue = new[]
                        //                                                                                                        {
                        //                                                                                                            new { _ = "tj0XtM/FsDzmOcHabTxkKcnH54KkZjkB72ah/utcaqA=" }
                        //                                                                                                        }
                        //                                                                                                    }
                        //                                                                                                },
                        //                                                                                                IssuerSerial = new[]
                        //                                                                                                {
                        //                                                                                                    new
                        //                                                                                                    {
                        //                                                                                                        X509IssuerName = new[] { new { _ = "CN=Trial LHDNM Sub CA V1, OU=Terms of use at http://www.posdigicert.com.my, O=LHDNM, C=MY" } },
                        //                                                                                                        X509SerialNumber = new[] { new { _ = "114094489988964920302056692430494377791" } }
                        //                                                                                                    }
                        //                                                                                                }
                        //                                                                                            }
                        //                                                                                        }
                        //                                                                                    }
                        //                                                                                }
                        //                                                                            }
                        //                                                                        }
                        //                                                                    }
                        //                                                                }
                        //                                                            }
                        //                                                        }
                        //                                                    }
                        //                                                },
                        //                                                KeyInfo = new[]
                        //                                                {
                        //                                                    new
                        //                                                    {
                        //                                                        X509Data = new[]
                        //                                                        {
                        //                                                            new
                        //                                                            {
                        //                                                                X509Certificate = new[]
                        //                                                                {
                        //                                                                    new { _ = "MIIFHzCCAwegAwIBAgIQVdXMqnRtSVpwGsGggAonPzANBgkqhkiG9w0BAQsFADB1MQswCQYDVQQGEwJNWTEOMAwGA1UEChMFTEhETk0xNjA0BgNVBAsTLVRlcm1zIG9mIHVzZSBhdCBodHRwOi8vd3d3LnBvc2RpZ2ljZXJ0LmNvbS5teTEeMBwGA1UEAxMVVHJpYWwgTEhETk0gU3ViIENBIFYxMB4XDTIyMDcwMTA4MzQxM1oXDTI0MDcwMTA4MzQxM1owSTELMAkGA1UEBhMCTVkxIzAhBgNVBAMTGk1PSEQgUkVEWlVBTiBCSU4gTU9IRCBUQUlCMRUwEwYDVQQFEww4MTEwMDYxNDYwNDMwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCNpRGjwDdMWIwHlKkJS1ZsNwEGIMJeMfwwjtoBzVj2a4D5Xt+P5hMnCvLwqujh3qnid7WeNmZtS509arZxiitWblUeeFGptXDUzt3ulp/ceZQlmJHhGgiKypxzETqSjnLZxZZWmvLlC+ZAWZF8nmt6cWDHXtffnaEWw2YokeANFx4OUJfvp1VsGVMJcVhZDdC3km5iItLGkVQCIZmSwn7dyEnG/zheOlzly4RBAJSaqfWPFHHoaaS5tsCDjAtwOWaop1RzU4fxiN3CoGmqdHOr4N40d8uPQZi4HGGBk6uOhd/wN2x6EgBeaEzan1jAUugnhfrwlT/FuoRYJmfoG55BAgMBAAGjgdYwgdMwEQYDVR0OBAoECEkjV6b8B33oMFMGA1UdIARMMEowSAYJKwYBBAGDikUBMDswOQYIKwYBBQUHAgEWLWh0dHBzOi8vd3d3LnBvc2RpZ2ljZXJ0LmNvbS5teS9yZXBvc2l0b3J5L2NwczATBgNVHSMEDDAKgAhNf9lrtsUI0DAOBgNVHQ8BAf8EBAMCA/gwRAYDVR0fBD0wOzA5oDegNYYzaHR0cDovL3RyaWFsY3JsLnBvc2RpZ2ljZXJ0LmNvbS5teS9UcmlhbExIRE5NVjEuY3JsMA0GCSqGSIb3DQEBCwUAA4ICAQCASolPfBpkv1bWInX1WepC9G6GbuI8Y/mx23Ktiu8AAFyn8l+nD7I5Er6V8lq4riS2QJvkiHin4G3THTrSHbx/9MQME8Vl/LPkiosI/I9+2lMDJaYRfZJ6CrgFTW1yfzwoCS/t9ZGeAbSZfgwq3MzXEgzmcpW7LWQtfxB+mw4nc9z6dvGU4Y8gJuKsspJvb4zCPxkyo5IxDW2MABxQnFeeSiMWvGWDe/1rrFPNv3tiJqP3+avwCSGmW7kEpcStpYuOaCvh4zefFVdlqIU7zMgr12+oft0xMFLhMSmWmzhEqSntZi0qY2jCp+v+QkxHy6JMAAYOmljb36siVM6u/uIXieEQNVABGnRrl+jpVRz45Z9ozXA647EEvA4G5R0wwwvL/bh/DWh2zA8yGNMsR0JQx+74BBDVaulV29h5lHW+IqrwChKzNj27aDE7SYqwHxaETTjp1nV1Zd8t9o8MvpXCWEsH194r06ebpCkMh3IxIR/jn01PRzjz7iaKEegCBpl23BRHXib8DMq5jK/z2CLmvHlJbNRRxd42psp7wVdX48YQhYCNdps/lJmgQxTo7HJFjirxpWdMUP7lxML21YDLCyAXOG0e4g00SqA8SJg3ROSzZXKZP0nvezSryXWmr8STTTGia+YjjynHoK8o0/uYgCW2SC+ZfhPH9gHJv9ZeNQ==" },
                        //                                                                },
                        //                                                                X509SubjectName = new[] {
                        //                                                                    new { _ = "SERIALNUMBER=811006146043, CN=MOHD REDZUAN BIN MOHD TAIB, C=MY" }
                        //                                                                },
                        //                                                                X509IssuerSerial = new[] {
                        //                                                                    new
                        //                                                                    {
                        //                                                                        X509IssuerName = new []
                        //                                                                        {
                        //                                                                            new
                        //                                                                            {
                        //                                                                                _ = "CN=Trial LHDNM Sub CA V1, OU=Terms of use at http://www.posdigicert.com.my, O=LHDNM, C=MY"
                        //                                                                            }
                        //                                                                        },
                        //                                                                        X509SerialNumber = new []
                        //                                                                        {
                        //                                                                            new
                        //                                                                            {
                        //                                                                                _ = "114094489988964920302056692430494377791"
                        //                                                                            }
                        //                                                                        }
                        //                                                                    }
                        //                                                                },
                        //                                                            }
                        //                                                        }
                        //                                                    }
                        //                                                },
                        //                                                SignatureValue = new[]
                        //                                                {
                        //                                                    new
                        //                                                    {
                        //                                                        _ = "Gw5uGjtok0IiPQ+hVH8R2xKFTojrm2fVM8P4wtcfgemqaJSAntAcacb8vcTbU6WAfLcIneEXRHTCG+qzawqBN6bjIl9yfsQ+IGReQhbZqQ43Zh2fFgPBwLo4Ywp4NqiGyGsrd/lm4PPtE8PmZbcQHTudHMYYxDDykA0ok1Lw6Xt/+lR7WHtp8+kHW37V8iPEDWAq5kfHazvke5kkzY+M/mWCzqlE4bIIiEk1lNjovGbER4K8XM3oMQUPpHkYi1P/UUMFP7QIAzMsLXKwmXx1rM77mfmOVrzNHgbetyfPJsfqiDrwQpB6KcODsui5CmhfrphMJs5gwcH7sG+6TNVn6w=="
                        //                                                    }
                        //                                                },
                        //                                                SignedInfo = new[]
                        //                                                {
                        //                                                    new
                        //                                                    {
                        //                                                        SignatureMethod = new[]
                        //                                                        {
                        //                                                            new { _ = "", Algorithm = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256" }
                        //                                                        },
                        //                                                        Reference = new[]
                        //                                                        {
                        //                                                            new
                        //                                                            {
                        //                                                                Type = "http://uri.etsi.org/01903/v1.3.2#SignedProperties",
                        //                                                                URI = "#id-xades-signed-props",
                        //                                                                DigestMethod = new[]
                        //                                                                {
                        //                                                                    new { _ = "", Algorithm = "http://www.w3.org/2001/04/xmlenc#sha256" }
                        //                                                                },
                        //                                                                DigestValue = new[]
                        //                                                                {
                        //                                                                    new { _ = "KEV8ZySPs6wrKyNcwVe7x3sgDwFVRLWWB3yZNSPky1I=" }  // Replace with actual digest
                        //                                                                }
                        //                                                            },
                        //                                                            new
                        //                                                            {
                        //                                                                Type = "",
                        //                                                                URI = "",
                        //                                                                DigestMethod = new[]
                        //                                                                {
                        //                                                                    new { _ = "", Algorithm = "http://www.w3.org/2001/04/xmlenc#sha256" }
                        //                                                                },
                        //                                                                DigestValue = new[]
                        //                                                                {
                        //                                                                    new { _ = "t+vl1+3vDeaganWrHMydIWhg7x/8xFQCa6pkBuLs2w0=" }  // Replace with actual digest
                        //                                                                }
                        //                                                            }
                        //                                                        }
                        //                                                    }
                        //                                                }
                        //                                            }
                        //                                        }
                        //                                    }
                        //                                }
                        //                            }
                        //                        }
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    },
                        //},
                        //Signature = new[]
                        //{
                        //    new
                        //    {
                        //        ID = new [] { new { _ = "urn:oasis:names:specification:ubl:signature:Invoice" } },
                        //        SignatureMethod = new [] { new { _ = "urn:oasis:names:specification:ubl:dsig:enveloped:xades" } }
                        //    }
                        //}
                    },
                ],
            };

            var supplier = new Supplier
            {
                Name = invoice.SupplierName,
                Tin = invoice.SupplierTIN,
                Brn = invoice.SupplierBRN,
                Address1 = invoice.SupplierAddressLine1,
                Address2 = invoice.SupplierAddressLine2,
                Address3 = invoice.SupplierAddressLine3,
                City = invoice.SupplierCity,
                PostalCode = invoice.SupplierPostalCode,
                State = invoice.SupplierCountrySubentityCode,
                CountryCode = invoice.SupplierCountryCode,
                IdType = supplierIdType,
                SstRegistrationNumber = supplierSst,
                TaxTourismRegistrationNumber = invoice.SupplierTTX,
                MsicCode = invoice.SupplierIndustryCode,
                BusinessActivityDescription = invoice.SupplierBusinessActivityDescription,
                Email = invoice.SupplierEmail,
                ContactNumber = invoice.SupplierTelephone
            };

            var customer = new Customer
            {
                Name = invoice.CustomerName,
                Tin = invoice.CustomerTIN,
                Brn = invoice.CustomerBRN,
                Address1 = invoice.CustomerAddressLine1,
                Address2 = invoice.CustomerAddressLine2,
                Address3 = invoice.CustomerAddressLine3,
                City = invoice.CustomerCity,
                PostalCode = invoice.CustomerPostalCode,
                State = invoice.CustomerCountrySubentityCode,
                CountryCode = invoice.CustomerCountryCode,
                Email = invoice.CustomerEmail,
                ContactNumber = invoice.CustomerTelephone,
                IdType = customerIdType,
                SstRegistrationNumber = customerSst,
            };

            var invoiceLine = invoice.ItemList.ConvertAll(item => new InvoiceLine
            {
                LineNumber = item.Id,
                Quantity = item.Qty,
                UnitPrice = item.UnitPrice,
                LineAmount = item.Subtotal,
                TaxAmount = item.TaxAmount,
                Description = item.Description,
                UnitCode = uomMappingDictionary[item.Unit],
                ClassificationCode = classificationMappingDictionary[item.ClassificationCode],
                CurrencyCode = invoice.CurrencyCode,
            });

            invoiceDocuments.Add(new InvoiceDocument
            {
                InvoiceTypeCode = invoice.InvoiceTypeCode,
                InvoiceNumber = invoice.Irn,
                IssueDate = now,
                DocumentCurrencyCode = invoice.CurrencyCode,
                TaxCurrencyCode = invoice.CurrencyCode,
                TotalAmount = invoice.TotalPayableAmount,
                TaxAmount = invoice.TaxAmount,
                Supplier = supplier,
                Customer = customer,
                InvoiceLines = invoiceLine,
                TotalExcludingTax = invoice.TotalExcludingTax,
                TotalIncludingTax = invoice.TotalIncludingTax,
                BillingReferenceId = invoice.BillingReferenceID,
                AdditionalDocumentReferenceID = invoice.AdditionalDocumentReferenceID
            });

            // Step 2: Convert document to JSON string
            var documentString = JsonConvert.SerializeObject(ublInvoice);

            // Step 3: Convert document string to Base64
            var documentBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(documentString));

            // Step 4: Generate SHA256 hash
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(documentString));
                var documentHashHex = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                // Step 5: Prepare payload for submission
                documentList.Add(new UblDocument
                {
                    Format = "JSON",
                    DocumentHash = documentHashHex,
                    CodeNumber = invoice.Irn,
                    Document = documentBase64,
                });
            }
        }
        var payload = new UblDocumentRequest
        {
            Documents = documentList.ToArray()
        };

        var response = await _lhdnApi.SubmitInvoiceAsync(payload, partnerTin);

        foreach (var doc in invoiceDocuments)
        {
            var matched = response?.AcceptedDocuments?.FirstOrDefault(d => d?.InvoiceCodeNumber == doc.InvoiceNumber);
            if (matched != null)
            {
                doc.Uuid = matched.Uuid;
                doc.SubmissionStatus = true;
                doc.DocumentStatus = DocumentStatus.Submitted;
            }
            else
            {
                doc.SubmissionStatus = false;
            }
        }

        await _invoiceDocumentRepository.AddRangeAsync(invoiceDocuments, cancellationToken);

        if (response?.AcceptedDocuments?.Any() == true)
        {
            return new Response<SubmitInvoiceResponse>(response, "Successfully submitted invoice(s).");
        }
        else
        {
            var errorMessages = new List<string>();
            if (response?.RejectedDocuments != null)
            {
                foreach (var rejectedDoc in response.RejectedDocuments)
                {
                    var error = rejectedDoc?.Error;
                    if (error?.Details != null && error.Details.Any())
                    {
                        foreach (var detail in error.Details)
                        {
                            errorMessages.Add($"{error?.Message ?? "Unknown Error"}: {detail?.Message ?? "No detail message"}");
                        }
                    }
                    else
                    {
                        errorMessages.Add(error?.Message ?? "Unknown error");
                    }
                }
            }

            return new Response<object>
            {
                Succeeded = false,
                Message = "Failed to submit some or all invoices.",
                Errors = errorMessages
            };
        }
    }

    private static string FormatPhoneNumber(string inputPhone)
    {
        if (string.IsNullOrWhiteSpace(inputPhone))
            return "+60187912826"; 

        if (inputPhone.Trim().StartsWith("+"))
            return inputPhone.Trim().Replace(" ", "").Replace("-", "");

        var cleanedPhone = new string(inputPhone.Where(char.IsDigit).ToArray());
        return "+" + cleanedPhone;
    }

    public static bool IsValidIdType(string? idType)
    {
        var validTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "NRIC", "BRN", "PASSPORT", "ARMY"
        };

        return !string.IsNullOrWhiteSpace(idType) && validTypes.Contains(idType.Trim());
    }
}
