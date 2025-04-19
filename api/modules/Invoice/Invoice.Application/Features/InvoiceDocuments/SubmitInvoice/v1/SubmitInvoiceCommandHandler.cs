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

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.SubmitInvoice.v1;

public sealed class SubmitInvoiceComamndHandler
    : IRequestHandler<SubmitInvoiceCommand, object>
{
    private readonly ILhdnApi _lhdnApi;
    private readonly IRepository<InvoiceDocument> _invoiceDocumentRepository;
    private readonly IRepository<UomMapping> _uomMappingRepository;
    private readonly ILogger<SubmitInvoiceComamndHandler> _logger;

    public SubmitInvoiceComamndHandler(ILhdnApi lhdnApi, [FromKeyedServices("invoice:invoiceDocuments")] IRepository<InvoiceDocument> invoiceDocumentRepository,
        [FromKeyedServices("invoice:uomMappings")] IRepository<UomMapping> uomMappingRepository, ILogger<SubmitInvoiceComamndHandler> logger)
    {
        _lhdnApi = lhdnApi;
        _invoiceDocumentRepository = invoiceDocumentRepository;
        _uomMappingRepository = uomMappingRepository;
        _logger = logger;
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

        // check submission is exist
        var existingInvoiceDocument = await _invoiceDocumentRepository
            .FirstOrDefaultAsync(new GetInvoiceDocumentByInvoiceNumber(request.Irn), cancellationToken);

        if (existingInvoiceDocument != null)
        {
            _logger.LogError($"Invoice with invoice number {request.Irn} has already been submitted.");
            return new Response<object>("Invoice has already been submitted.");
        }

        // get user uom mapping
        var userRequestUomCodes = request.ItemList
            .Select(i => i.Unit)
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
                    Id = [new() { _ = request.Irn }],
                    IssueDate = [new() { _ = now.ToString("yyyy-MM-dd") }],
                    IssueTime = [new() { _ = now.ToString("HH:mm:ss") + "Z" }],
                    InvoiceTypeCode =
                    [
                        new() { _ = request.InvoiceTypeCode, ListVersionId = "1.0" },
                    ],
                    DocumentCurrencyCode = [new() { _ = request.CurrencyCode }],
                    InvoicePeriod =
                    [
                        new()
                        {
                            StartDate = [new() { _ = request.StartDate }],
                            EndDate = [new() { _ = request.EndDate }],
                            Description = [new() { _ = request.InvoicePeriodDescription }],
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
                                        new BasicComponent { _ = request.BillingReferenceID ?? "" },
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
                                    _ = request.AdditionalDocumentReferenceID ?? "",
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
                                    _ = request.SupplierAdditionalAccountID ?? "",
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
                                            _ = request.SupplierIndustryCode,
                                            Name = "Provision of telecommunications services",
                                        },
                                    ],
                                    PartyIdentification =
                                    [
                                        new()
                                        {
                                            Id =
                                            [
                                                new() { _ = request.SupplierTIN, SchemeId = "TIN" },
                                            ],
                                        },
                                        new()
                                        {
                                            Id =
                                            [
                                                new()
                                                {
                                                    _ = request.SupplierBRN,
                                                    SchemeId = "NRIC",
                                                },
                                            ],
                                        },
                                        new()
                                        {
                                            Id =
                                            [
                                                new() { _ = request.SupplierSST, SchemeId = "SST" },
                                            ],
                                        },
                                        new()
                                        {
                                            Id =
                                            [
                                                new() { _ = request.SupplierTTX, SchemeId = "TTX" },
                                            ],
                                        },
                                    ],
                                    PostalAddress =
                                    [
                                        new()
                                        {
                                            CityName = [new() { _ = request.SupplierCity }],
                                            PostalZone = [new() { _ = request.SupplierPostalCode }],
                                            CountrySubentityCode =
                                            [
                                                new() { _ = request.SupplierCountrySubentityCode },
                                            ],
                                            AddressLine =
                                            [
                                                new()
                                                {
                                                    Line =
                                                    [
                                                        new() { _ = request.SupplierAddressLine1 },
                                                    ],
                                                },
                                                new()
                                                {
                                                    Line =
                                                    [
                                                        new() { _ = request.SupplierAddressLine2 },
                                                    ],
                                                },
                                                new()
                                                {
                                                    Line =
                                                    [
                                                        new() { _ = request.SupplierAddressLine3 },
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
                                                            _ = request.SupplierCountryCode,
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
                                            RegistrationName = [new() { _ = request.SupplierName }],
                                        },
                                    ],
                                    Contact =
                                    [
                                        new()
                                        {
                                            Telephone = [new() { _ = request.SupplierTelephone }],
                                            ElectronicMail = [new() { _ = request.SupplierEmail }],
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
                                                new() { _ = request.CustomerTIN, SchemeId = "TIN" },
                                            ],
                                        },
                                        new()
                                        {
                                            Id =
                                            [
                                                new()
                                                {
                                                    _ = request.CustomerBRN ?? "",
                                                    SchemeId = "BRN",
                                                },
                                            ],
                                        },
                                    ],
                                    PostalAddress =
                                    [
                                        new()
                                        {
                                            CityName = [new() { _ = request.CustomerCity }],
                                            PostalZone = [new() { _ = request.CustomerPostalCode }],
                                            CountrySubentityCode =
                                            [
                                                new() { _ = request.CustomerCountrySubentityCode },
                                            ],
                                            AddressLine =
                                            [
                                                new()
                                                {
                                                    Line =
                                                    [
                                                        new() { _ = request.CustomerAddressLine1 },
                                                    ],
                                                },
                                                new()
                                                {
                                                    Line =
                                                    [
                                                        new() { _ = request.CustomerAddressLine2 },
                                                    ],
                                                },
                                                new()
                                                {
                                                    Line =
                                                    [
                                                        new() { _ = request.CustomerAddressLine3 },
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
                                                            _ = request.CustomerCountryCode,
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
                                            RegistrationName = [new() { _ = request.CustomerName }],
                                        },
                                    ],
                                    Contact =
                                    [
                                        new()
                                        {
                                            Telephone = [new() { _ = request.CustomerTelephone }],
                                            ElectronicMail = [new() { _ = request.CustomerEmail }],
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
                                    _ = request.TotalAmount,
                                    CurrencyId = request.CurrencyCode,
                                },
                            ],
                            TaxExclusiveAmount =
                            [
                                new() { _ = 0, CurrencyId = request.CurrencyCode },
                            ],
                            TaxInclusiveAmount =
                            [
                                new() { _ = 0, CurrencyId = request.CurrencyCode },
                            ],
                        },
                    ],
                    InvoiceLine =
                    [
                        .. request.ItemList.ConvertAll(item => new Dtos.Ubl.Invoice.InvoiceLine()
                        {
                            Id = [new() { _ = item.Id.ToString() }],
                            InvoicedQuantity = [new() { _ = item.Qty, UnitCode = uomMappingDictionary[item.Unit] }],
                            LineExtensionAmount =
                            [
                                new() { _ = item.TotItemVal, CurrencyId = request.CurrencyCode },
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
                                                new() { _ = "001", ListId = "CLASS" },
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
                                            CurrencyId = request.CurrencyCode,
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
                                            CurrencyId = request.CurrencyCode,
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
                                                    CurrencyId = request.CurrencyCode,
                                                },
                                            ],
                                            TaxAmount =
                                            [
                                                new()
                                                {
                                                    _ = item.TaxAmount,
                                                    CurrencyId = request.CurrencyCode,
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
                                            _ = item.TotItemVal,
                                            CurrencyId = request.CurrencyCode,
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
                                    _ = Convert.ToDecimal(request.TaxAmount),
                                    CurrencyId = request.CurrencyCode,
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
                                            _ = Convert.ToDecimal(request.TaxableAmount),
                                            CurrencyId = request.CurrencyCode,
                                        },
                                    ],
                                    TaxAmount =
                                    [
                                        new()
                                        {
                                            _ = Convert.ToDecimal(request.TaxAmount),
                                            CurrencyId = request.CurrencyCode,
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

        // Step 2: Convert document to JSON string
        var documentString = JsonConvert.SerializeObject(ublInvoice);

        // Step 3: Convert document string to Base64
        var documentBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(documentString));

        // Step 4: Generate SHA256 hash
        UblDocumentRequest payload = null;
        using (var sha256 = SHA256.Create())
        {
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(documentString));
            var documentHashHex = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            // Step 5: Prepare payload for submission
            payload = new()
            {
                Documents =
                [
                    new()
                    {
                        Format = "JSON",
                        DocumentHash = documentHashHex,
                        CodeNumber = request.Irn,
                        Document = documentBase64,
                    },
                ],
            };
        }

        var response = await _lhdnApi.SubmitInvoiceAsync(payload);

        // submission success
        if (response.AcceptedDocuments.Count > 0)
        {
            // store data to db
            var supplier = new Supplier
            {
                Name = request.SupplierName,
                Tin = request.SupplierTIN,
                Brn = request.SupplierBRN,
                Address = FormatAddress(request.SupplierAddressLine1, request.SupplierAddressLine2, request.SupplierAddressLine3),
                City = request.SupplierCity,
                PostalCode = request.SupplierPostalCode,
                CountryCode = request.SupplierCountryCode
            };

            var customer = new Customer
            {
                Name = request.CustomerName,
                Tin = request.CustomerTIN,
                Brn = request.CustomerBRN,
                Address = FormatAddress(request.CustomerAddressLine1, request.CustomerAddressLine2, request.CustomerAddressLine3),
                City = request.CustomerCity,
                PostalCode = request.CustomerPostalCode,
                CountryCode = request.CustomerCountryCode
            };

            var invoiceLine = request.ItemList.ConvertAll(item => new InvoiceLine()
            {
                LineNumber = item.Id,
                Quantity = item.Qty,
                UnitPrice = item.UnitPrice,
                LineAmount = item.TotItemVal,
                Description = item.Description,
                UnitCode = uomMappingDictionary[item.Unit],
                CurrencyCode = request.CurrencyCode
            });

            var invoiceDocument = new InvoiceDocument()
            {
                Uuid = response.AcceptedDocuments.FirstOrDefault()?.Uuid,
                InvoiceNumber = request.BillingReferenceID,
                IssueDate = now,
                DocumentCurrencyCode = request.CurrencyCode,
                TaxCurrencyCode = request.CurrencyCode,
                TotalAmount = request.TotalAmount,
                TaxAmount = request.TaxAmount,
                Supplier = supplier,
                SupplierId = supplier.Id,
                Customer = customer,
                CustomerId = customer.Id,
                InvoiceLines = invoiceLine
            };

            await _invoiceDocumentRepository.AddAsync(invoiceDocument, cancellationToken);
            return new Response<SubmitInvoiceResponse>(response, "Successfully submit invoice");
        }
        else
        {
            var errorMessages = new List<string>();
            if (response.RejectedDocuments != null)
            {
                foreach (var rejectedDoc in response.RejectedDocuments)
                {
                    var error = rejectedDoc?.Error;
                    if (error?.Details != null && error.Details.Any())
                    {
                        foreach (var detail in error.Details)
                        {
                            var message = $"{error?.Message ?? "Unknown Error"}: {detail?.Message ?? "No detail message"}";
                            errorMessages.Add(message);
                        }
                    }
                    else
                    {
                        var message = $"{error?.Message ?? "Unknown Error"}";
                        errorMessages.Add(message);
                    }
                }
            }

            var errorResponse = new Response<object>
            {
                Succeeded = false,
                Errors = errorMessages,
                Message = "Failed to submit invoice"
            };

            return errorResponse;
        }
    }

    public static string FormatAddress(string line1, string line2, string line3)
    {
        var addressParts = new[]
        {
            line1,
            line2,
            line3
        };

        return string.Join(", ", addressParts.Where(part => !string.IsNullOrWhiteSpace(part)));
    }
}
