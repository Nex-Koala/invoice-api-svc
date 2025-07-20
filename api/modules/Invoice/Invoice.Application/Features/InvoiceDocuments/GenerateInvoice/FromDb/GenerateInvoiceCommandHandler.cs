using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NexKoala.Framework.Core.Exceptions;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Specifications;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Specifications;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.WebApi.Invoice.Domain.Settings;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GenerateInvoice.FromDb;

public sealed class GenerateInvoiceCommandHandler(
    ILhdnApi lhdnApi,
    ILogger<GenerateInvoiceCommandHandler> logger,
    IOptions<EInvoiceSettings> options,
    IInvoiceService invoiceService,
    [FromKeyedServices("invoice:invoiceDocuments")] IRepository<InvoiceDocument> invoiceDocumentRepository,
    [FromKeyedServices("invoice:partners")] IRepository<Partner> partnerRepository
) : IRequestHandler<GenerateInvoiceCommand, Response<byte[]>>
{
    public async Task<Response<byte[]>> Handle(
        GenerateInvoiceCommand request,
        CancellationToken cancellationToken
    )
    {
        string templatePath = options.Value.TemplatePath ?? throw new GenericException("InvoiceTemplatePath is not configured");

        if (!Path.IsPathRooted(templatePath))
        {
            string basePath = Directory.GetCurrentDirectory();
            templatePath = Path.GetFullPath(Path.Combine(basePath, templatePath));
        }

        var invoiceDocument = await invoiceDocumentRepository.FirstOrDefaultAsync(new GetInvoiceDocumentByUuid(request.Uuid), cancellationToken);
        if (invoiceDocument == null)
        {
            throw new GenericException("Invoice not found");
        }

        // get user TIN
        string partnerTin;
        if (request.IsAdmin ?? false)
        {
            partnerTin = options.Value.AdminTin;
        }
        else
        {
            var partner = await partnerRepository.FirstOrDefaultAsync(new PartnerByUserIdSpec(request.UserId), cancellationToken);

            if (partner == null)
            {
                return new Response<byte[]>($"Partner not found for user ID {request.UserId}.");
            }

            if (string.IsNullOrWhiteSpace(partner.Tin))
            {
                return new Response<byte[]>($"The TIN (Tax Identification Number) for {partner.Name} is not set.");
            }

            partnerTin = partner.Tin;
        }

        var baseUrl = options.Value.MyInvoiceBaseUrl;
        string invoiceXmlTree = GenerateInvoiceXml(invoiceDocument, baseUrl, invoiceService);

        byte[] pdfBytes = invoiceService.GenerateInvoice(invoiceXmlTree, templatePath);

        return new Response<byte[]>(pdfBytes);
    }

    public static string GetInvoiceTypeDescriptionByCode(string code)
    {
        var data = new Dictionary<string, string>
        {
            { "01", "Invoice" },
            { "02", "Credit Note" },
            { "03", "Debit Note" },
            { "04", "Refund Note" },
            { "11", "Self-billed Invoice" },
            { "12", "Self-billed Credit Note" },
            { "13", "Self-billed Debit Note" },
            { "14", "Self-billed Refund Note" },
        };

        if (data.TryGetValue(code, out string description))
        {
            return description;
        }
        else
        {
            return "";
        }
    }

    public static string GenerateInvoiceXml(
        InvoiceDocument invoiceDocument,
        string baseUrl,
        IInvoiceService invoiceService
    )
    {
        string GetValueOrDash(string? value) => string.IsNullOrWhiteSpace(value) ? "-" : value;
        string FormatDecimal(decimal value) => value.ToString("F2");
        string FormatDateTime(DateTimeOffset dateTime) => dateTime.ToString("yyyy-MM-dd HH:mm:ss");

        // e-invoice info
        var eInvoiceType = $"{invoiceDocument.InvoiceTypeCode} - {GetInvoiceTypeDescriptionByCode(invoiceDocument.InvoiceTypeCode)}";
        var invoiceDateTime = FormatDateTime(invoiceDocument.IssueDate);
        var dateTimeValidated = FormatDateTime(invoiceDocument.DateTimeValidated ?? invoiceDocument.IssueDate);

        // Supplier info
        var supplier = invoiceDocument.Supplier;
        var supplierInfo = new Dictionary<string, string>
        {
            ["supplierName"] = GetValueOrDash(supplier.Name),
            ["supplierAddress"] = GetValueOrDash(FormatAddress(supplier.Address1, supplier.Address2, supplier.Address3, supplier.PostalCode, supplier.City)),
            ["supplierAddress1"] = GetValueOrDash(supplier.Address1),
            ["supplierAddress2"] = GetValueOrDash(supplier.Address2),
            ["supplierAddress3"] = GetValueOrDash(supplier.Address3),
            ["supplierCity"] = GetValueOrDash(supplier.City),
            ["supplierPostalCode"] = GetValueOrDash(supplier.PostalCode),
            ["supplierCountryCode"] = GetValueOrDash(supplier.CountryCode),
            ["supplierContact"] = GetValueOrDash(supplier.ContactNumber),
            ["supplierEmail"] = GetValueOrDash(supplier.Email),
            ["supplierTIN"] = GetValueOrDash(supplier.Tin),
            ["supplierRegNo"] = GetValueOrDash(supplier.Brn),
            ["supplierSST"] = GetValueOrDash(supplier.SstRegistrationNumber),
            ["supplierMSIC"] = GetValueOrDash(supplier.MsicCode),
            ["businessDescription"] = GetValueOrDash(supplier.BusinessActivityDescription)
        };

        // Buyer info
        var customer = invoiceDocument.Customer;
        var buyerInfo = new Dictionary<string, string>
        {
            ["buyerTIN"] = GetValueOrDash(customer.Tin),
            ["buyerName"] = GetValueOrDash(customer.Name),
            ["buyerRegNo"] = GetValueOrDash(customer.Brn),
            ["buyerAddress"] = GetValueOrDash(FormatAddress(customer.Address1, customer.Address2, customer.Address3, customer.PostalCode, customer.City)),
            ["buyerAddress1"] = GetValueOrDash(customer.Address1),
            ["buyerAddress2"] = GetValueOrDash(customer.Address2),
            ["buyerAddress3"] = GetValueOrDash(customer.Address3),
            ["buyerCity"] = GetValueOrDash(customer.City),
            ["buyerPostalCode"] = GetValueOrDash(customer.PostalCode),
            ["buyerCountryCode"] = GetValueOrDash(customer.CountryCode),
            ["buyerContactNumber"] = GetValueOrDash(customer.ContactNumber),
            ["buyerEmail"] = GetValueOrDash(customer.Email)
        };

        // QR Code
        var qrLink = $"{baseUrl}/{invoiceDocument.Uuid}/share/{invoiceDocument.LongId}";
        var qrCodeBase64 = invoiceService.GenerateQRCode(qrLink);

        // Items
        var itemsElement = new XElement("items",
            invoiceDocument.InvoiceLines.Select(item =>
                new XElement("item",
                    new XElement("classificationCode", "001"), // Replace if classification exists
                    new XElement("description", item.Description),
                    new XElement("quantity", item.Quantity),
                    new XElement("unitPrice", FormatDecimal(item.UnitPrice)),
                    new XElement("amount", FormatDecimal(item.Quantity * item.UnitPrice)),
                    new XElement("discount", ""), // Add real discount if applicable
                    new XElement("taxRate", ""),   // Add real tax rate if applicable
                    new XElement("taxAmount", FormatDecimal(item.TaxAmount)),
                    new XElement("totalPrice", FormatDecimal(item.LineAmount)),
                    new XElement("currencyCode", item.CurrencyCode)
                )
            )
        );

        // Construct main XML
        var invoiceElement = new XElement("invoice");

        // Append supplier info
        foreach (var (key, value) in supplierInfo)
            invoiceElement.Add(new XElement(key, value));

        // Append e-invoice info
        invoiceElement.Add(
            new XElement("eInvoiceType", eInvoiceType),
            new XElement("eInvoiceVersion", "1.0"),
            new XElement("eInvoiceCode", invoiceDocument.InvoiceNumber),
            new XElement("uuid", invoiceDocument.Uuid),
            new XElement("originalInvoiceRefNo", "Not Applicable"),
            new XElement("invoiceDateTime", invoiceDateTime)
        );

        // Append buyer info
        foreach (var (key, value) in buyerInfo)
            invoiceElement.Add(new XElement(key, value));

        // Append items
        invoiceElement.Add(itemsElement);

        // Append totals
        invoiceElement.Add(
            new XElement("currencyCode", invoiceDocument.DocumentCurrencyCode),
            new XElement("subtotal", FormatDecimal(invoiceDocument.TotalAmount)),
            new XElement("totalExcludingTax", FormatDecimal(invoiceDocument.TotalExcludingTax)),
            new XElement("taxAmount", FormatDecimal(invoiceDocument.TaxAmount)),
            new XElement("totalIncludingTax", FormatDecimal(invoiceDocument.TotalIncludingTax)),
            new XElement("totalPayableAmount", FormatDecimal(invoiceDocument.TotalAmount)),
            new XElement("digitalSignature", ""), // Populate if digital signing available
            new XElement("dateTimeValidated", dateTimeValidated),
            new XElement("QRCode", qrCodeBase64)
        );

        var xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), invoiceElement);
        return xDoc.ToString();
    }

    private static string FormatAddress(string? address1, string? address2, string? address3, string? postalCode, string? city)
    {
        var parts = new[] { address1, address2, address3, postalCode, city }
            .Where(p => !string.IsNullOrWhiteSpace(p))
            .Select(p => p!.Trim().TrimEnd(','));

        return string.Join(", ", parts);
    }

}
