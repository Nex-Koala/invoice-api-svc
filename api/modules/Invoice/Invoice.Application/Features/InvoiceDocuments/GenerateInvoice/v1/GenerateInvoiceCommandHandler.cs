using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NexKoala.Framework.Core.Exceptions;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Document;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Domain.Settings;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GenerateInvoice.v1;

public sealed class GenerateInvoiceCommandHandler(
    ILhdnApi lhdnApi,
    ILogger<GenerateInvoiceCommandHandler> logger,
    IOptions<EInvoiceSettings> options,
    IInvoiceService invoiceService
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

        var rawDocument = await lhdnApi.GetDocumentAsync(request.Uuid);
        var baseUrl = options.Value.ApiBaseUrl;
        string invoiceXmlTree = GenerateInvoiceXml(rawDocument, baseUrl, invoiceService);

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
        RawDocument invoice,
        string baseUrl,
        IInvoiceService invoiceService
    )
    {
        var ublInvoice = invoice.Document.Invoice[0];

        // e-invoice info
        var invoiceTypeCode = ublInvoice.InvoiceTypeCode[0]._;
        var invoiceTypeDesc = GetInvoiceTypeDescriptionByCode(invoiceTypeCode);
        var eInvoiceType = invoiceTypeCode + " - " + invoiceTypeDesc;

        var eInvoiceVersion = ublInvoice.InvoiceTypeCode[0].ListVersionId;
        var eInvoiceCode = ublInvoice.Id[0]._;
        var uuid = invoice.Uuid;
        var originalInvoiceRefNo = "Not Applicable";
        var invoiceDateTime = invoice.DateTimeIssued.ToString("yyyy-MM-dd HH:mm:ss");

        // suplier details
        var supplierName = ublInvoice
            .AccountingSupplierParty[0]
            .Party[0]
            .PartyLegalEntity[0]
            .RegistrationName[0]
            ._;
        var supplierAddressLines = ublInvoice
            .AccountingSupplierParty[0]
            .Party[0]
            .PostalAddress[0]
            .AddressLine.Select(a => a.Line[0]._);

        var city = ublInvoice.AccountingSupplierParty[0].Party[0].PostalAddress[0].CityName[0]._;
        var postalCode = ublInvoice
            .AccountingSupplierParty[0]
            .Party[0]
            .PostalAddress[0]
            .PostalZone[0]
            ._;

        var supplierAddress =
            string.Join(", ", supplierAddressLines.Where(line => !string.IsNullOrEmpty(line)))
            + (string.IsNullOrWhiteSpace(postalCode) ? "" : $", {postalCode}")
            + (string.IsNullOrWhiteSpace(city) ? "" : $", {city}");

        var supplierContact = ublInvoice
            .AccountingSupplierParty[0]
            .Party[0]
            .Contact[0]
            .Telephone[0]
            ._;

        var supplierEmail = ublInvoice
            .AccountingSupplierParty[0]
            .Party[0]
            .Contact[0]
            .ElectronicMail[0]
            ._;

        var supplierTIN = ublInvoice
            .AccountingSupplierParty[0]
            .Party[0]
            .PartyIdentification.FirstOrDefault(p => p.Id.Any(id => id.SchemeId == "TIN"))
            ?.Id.FirstOrDefault(id => id.SchemeId == "TIN")
            ?._;

        var supplierRegNo = ublInvoice
            .AccountingSupplierParty[0]
            .Party[0]
            .PartyIdentification.FirstOrDefault(p =>
                p.Id.Any(id =>
                    id.SchemeId == "NRIC"
                    || id.SchemeId == "BRN"
                    || id.SchemeId == "PASSPORT"
                    || id.SchemeId == "ARMY"
                )
            )
            ?.Id.FirstOrDefault(id =>
                id.SchemeId == "NRIC"
                || id.SchemeId == "BRN"
                || id.SchemeId == "PASSPORT"
                || id.SchemeId == "ARMY"
            )
            ?._;

        var supplierSST = ublInvoice
            .AccountingSupplierParty[0]
            .Party[0]
            .PartyIdentification.FirstOrDefault(p => p.Id.Any(id => id.SchemeId == "SST"))
            ?.Id.FirstOrDefault(id => id.SchemeId == "SST")
            ?._;

        var supplierMSIC = ublInvoice
            .AccountingSupplierParty[0]
            .Party[0]
            .IndustryClassificationCode[0]
            ._;

        var supplierBusinessDescription = ublInvoice
            .AccountingSupplierParty[0]
            .Party[0]
            .IndustryClassificationCode[0]
            .Name;

        // buyer details
        var buyerTIN =
            ublInvoice
                .AccountingCustomerParty[0]
                .Party[0]
                .PartyIdentification.FirstOrDefault(p => p.Id.Any(id => id.SchemeId == "TIN"))
                ?.Id.FirstOrDefault(id => id.SchemeId == "TIN")
                ?._ ?? "-";

        var buyerRegNo =
            ublInvoice
                .AccountingCustomerParty[0]
                .Party[0]
                .PartyIdentification.FirstOrDefault(p =>
                    p.Id.Any(id =>
                        id.SchemeId == "NRIC"
                        || id.SchemeId == "BRN"
                        || id.SchemeId == "PASSPORT"
                        || id.SchemeId == "ARMY"
                    )
                )
                ?.Id.FirstOrDefault(id =>
                    id.SchemeId == "NRIC"
                    || id.SchemeId == "BRN"
                    || id.SchemeId == "PASSPORT"
                    || id.SchemeId == "ARMY"
                )
                ?._ ?? "-";

        var buyerName = ublInvoice
            .AccountingCustomerParty[0]
            .Party[0]
            .PartyLegalEntity[0]
            .RegistrationName[0]
            ._;

        var buyerAddressLines = ublInvoice
            .AccountingCustomerParty[0]
            .Party[0]
            .PostalAddress[0]
            .AddressLine.Select(a => a.Line[0]._);

        var buyerCity = ublInvoice
            .AccountingCustomerParty[0]
            .Party[0]
            .PostalAddress[0]
            .CityName[0]
            ._;
        var buyerPostalCode = ublInvoice
            .AccountingCustomerParty[0]
            .Party[0]
            .PostalAddress[0]
            .PostalZone[0]
            ._;

        var buyerAddress =
            string.Join(", ", buyerAddressLines.Where(line => !string.IsNullOrEmpty(line)))
            + (string.IsNullOrWhiteSpace(buyerPostalCode) ? "" : $", {buyerPostalCode}")
            + (string.IsNullOrWhiteSpace(buyerCity) ? "" : $", {buyerCity}");

        var buyerContact = ublInvoice.AccountingCustomerParty[0].Party[0].Contact[0].Telephone[0]._;

        var buyerEmail = ublInvoice
            .AccountingCustomerParty[0]
            .Party[0]
            .Contact[0]
            .ElectronicMail[0]
            ._;

        var qrLink = $"{baseUrl}/{uuid}/share/{invoice.LongId}";
        string qrCodeBase64 = invoiceService.GenerateQRCode(qrLink);

        var invoiceElement = new XElement(
            "invoice",
            new XElement("supplierName", supplierName),
            new XElement("supplierAddress", supplierAddress),
            new XElement("supplierContact", supplierContact),
            new XElement("supplierEmail", supplierEmail),
            new XElement("supplierTIN", supplierTIN),
            new XElement("supplierRegNo", supplierRegNo),
            new XElement("supplierSST", supplierSST),
            new XElement("supplierMSIC", supplierMSIC),
            new XElement("businessDescription", supplierBusinessDescription),
            new XElement("eInvoiceType", eInvoiceType),
            new XElement("eInvoiceVersion", eInvoiceVersion),
            new XElement("eInvoiceCode", eInvoiceCode),
            new XElement("uuid", uuid),
            new XElement("originalInvoiceRefNo", originalInvoiceRefNo),
            new XElement("invoiceDateTime", invoiceDateTime),
            new XElement("buyerTIN", buyerTIN),
            new XElement("buyerName", buyerName),
            new XElement("buyerRegNo", buyerRegNo),
            new XElement("buyerAddress", buyerAddress),
            new XElement("buyerContactNumber", buyerContact),
            new XElement("buyerEmail", buyerEmail),
            new XElement(
                "items",
                from item in invoice.Document.Invoice[0].InvoiceLine
                select new XElement(
                    "item",
                    new XElement(
                        "classificationCode",
                        item.Item[0].CommodityClassification[0].ItemClassificationCode[0]._
                    ),
                    new XElement("description", item.Item[0].Description[0]._),
                    new XElement("quantity", item.InvoicedQuantity[0]._),
                    new XElement("unitPrice", item.Price[0].PriceAmount[0]._.ToString("F2")),
                    new XElement(
                        "amount",
                        (item.InvoicedQuantity[0]._ * item.Price[0].PriceAmount[0]._).ToString("F2")
                    ),
                    new XElement("discount", ""),
                    new XElement("taxRate", ""),
                    new XElement("taxAmount", item.TaxTotal[0].TaxAmount[0]._.ToString("F2")),
                    new XElement("totalPrice", item.LineExtensionAmount[0]._.ToString("F2")),
                    new XElement("currencyCode", "RM")
                )
            ),
            new XElement("currencyCode", "RM"),
            new XElement("subtotal", invoice.TotalNetAmount.ToString("F2")),
            new XElement("totalExcludingTax", invoice.TotalExcludingTax.ToString("F2")),
            new XElement("taxAmount", ublInvoice.TaxTotal[0].TaxAmount[0]._.ToString("F2")),
            new XElement(
                "totalIncludingTax",
                ublInvoice.LegalMonetaryTotal[0].TaxInclusiveAmount[0]._.ToString("F2")
            ),
            new XElement("totalPayableAmount", invoice.TotalPayableAmount.ToString("F2")),
            new XElement("digitalSignature", ""),
            new XElement(
                "dateTimeValidated",
                invoice.DateTimeValidated.ToString("yyyy-MM-dd HH:mm:ss")
            ),
            new XElement("QRCode", qrCodeBase64)
        );

        var xDocument = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), invoiceElement);
        return xDocument.ToString();
    }
}
