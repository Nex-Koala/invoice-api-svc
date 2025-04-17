using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;
using System.Xml;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using iText.Html2pdf;
using QRCoder;
using NexKoala.WebApi.Invoice.Infrastructure.Persistence;
using NexKoala.WebApi.Invoice.Infrastructure.Helpers;
using NexKoala.WebApi.Invoice.Domain.Entities.OE;
using Microsoft.EntityFrameworkCore;
using NexKoala.WebApi.Invoice.Domain.Entities.PO;
using NexKoala.WebApi.Invoice.Application.Dtos;

namespace NexKoala.WebApi.Invoice.Infrastructure.Services;
public class InvoiceService(ClientDbContext dbContext, TrimStringService trimStringService) : IInvoiceService
{
    public byte[] GenerateInvoice(string xmlContent, string xsltTemplatePath)
    {
        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(xsltTemplatePath);

        string transformedHtml;
        using (StringWriter stringWriter = new StringWriter())
        {
            XmlWriterSettings writerSettings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                ConformanceLevel = ConformanceLevel.Auto,
                Indent = true
            };

            using (XmlReader xmlReader = XmlReader.Create(new StringReader(xmlContent)))
            using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, writerSettings))
            {
                xslt.Transform(xmlReader, xmlWriter);
            }
            transformedHtml = stringWriter.ToString();
        }

        byte[] pdfBytes;
        using (MemoryStream pdfStream = new MemoryStream())
        {
            HtmlConverter.ConvertToPdf(transformedHtml, pdfStream);
            pdfBytes = pdfStream.ToArray();
            return pdfBytes;
        }
    }

    public string GenerateQRCode(string data)
    {
        using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
        {
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q))
            {
                using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
                {
                    byte[] qrCodeBytes = qrCode.GetGraphic(20);
                    return "data:image/png;base64," + Convert.ToBase64String(qrCodeBytes);
                }
            }
        }
    }

    /// <summary>
    /// Retrieves sales invoices from Order Entry tables.
    /// Table Reference 
    /// OE => OEINVH (Order Entry Header) and OEINVD (Order Entry Detail)
    /// AR => ARIBH (Receivable Invoice Header) and ARIBD (Receivable Invoice Detail)
    ///       Fields: Item Description (TEXTDESC), Amounts (AMTEXTN), Tax (AMTTAX1)
    /// AP => APIBH (Payable Invoice Header) and APIBD (Payable Invoice Detail)
    ///       NA??
    /// </summary>
    public async Task<PaginatedResult<OrderEntryHeader>> GetSalesInvoices(int page = 1,
            int pageSize = 10,
            decimal? invoiceNumber = null)
    {
        var query = dbContext.OrderEntryHeaders
                    .Include(h => h.OrderEntryDetails)
                    .AsQueryable();

        if (invoiceNumber.HasValue)
        {
            query = query.Where(h => EF.Functions.Like(h.INVNUMBER, $"%{invoiceNumber}%"));
        }

        var paginatedResult = await PaginationHelper.PaginateAsync(query, page, pageSize);

        foreach (var header in paginatedResult.Data)
        {
            // Fetch the BRN dynamically from ARCUST table
            var customer = await dbContext.AccountReceivableCustomers
                .FirstOrDefaultAsync(c => c.IDCUST == header.CUSTOMER.Trim());
            header.CustomerBRN = customer?.BRN;

            trimStringService.TrimStringProperties(header);
            header.OrderEntryDetails = header.OrderEntryDetails
                .Select(detail => trimStringService.TrimStringProperties(detail))
                .ToList();
        }
        return paginatedResult;
    }

    /// <summary>
    /// Retrieves credit/debit notes from Order Entry tables.
    /// OE => OECRDH (Order Entry Credit/Debit Note Header) and OECRDD (Order Entry Credit/Debit Note Detail)
    /// AR => ARIBH (Receivable Invoice Header) and ARIBD (Receivable Invoice Detail)
    ///       Fields: Credit Details (TEXTDESC), Adjustments (AMTEXTN)
    /// AP => APIBH (Payable Invoice Header) and APIBD (Payable Invoice Detail)
    ///       Fields: Description (TEXTDESC), Tax (AMTTAX1)
    /// </summary>
    public async Task<PaginatedResult<OrderCreditDebitHeader>> GetCreditDebitNotes(int page = 1,
            int pageSize = 10,
            decimal? sequenceNumber = null)
    {
        var query = dbContext.OrderCreditDebitHeaders
                .Include(h => h.OrderCreditDebitDetails)
                .AsQueryable();

        if (sequenceNumber.HasValue)
        {
            query = query.Where(h => EF.Functions.Like(h.INVNUMBER, $"%{sequenceNumber}%"));
        }

        var paginatedResult = await PaginationHelper.PaginateAsync(query, page, pageSize);

        foreach (var note in paginatedResult.Data)
        {
            var customer = await dbContext.AccountReceivableCustomers
                .FirstOrDefaultAsync(c => c.IDCUST == note.CUSTOMER.Trim());
            note.CustomerBRN = customer?.BRN;

            trimStringService.TrimStringProperties(note);
            note.OrderCreditDebitDetails = note.OrderCreditDebitDetails
                .Select(detail => trimStringService.TrimStringProperties(detail))
                .ToList();
        }
        return paginatedResult;
    }

    /// <summary>
    /// Retrieves purchase invoices (self-billing).
    /// PO => POINVH1 (Purchase Invoice Header) and POINVL (Purchase Invoice Line)
    /// AP => APIBH (Accounts Payable Invoice Header) and APIBD (Accounts Payable Invoice Detail)
    /// </summary>
    public async Task<PaginatedResult<PurchaseInvoiceHeader>> GetPurchaseInvoices(
        int page = 1,
        int pageSize = 10,
        string? invoiceNumber = null)
    {
        var query = dbContext.PurchaseInvoiceHeaders
                .Include(h => h.PurchaseInvoiceDetails)
                .AsQueryable();

        if (!string.IsNullOrEmpty(invoiceNumber))
        {
            query = query.Where(h => EF.Functions.Like(h.INVNUMBER, $"%{invoiceNumber}%"));
        }

        var paginatedResult = await PaginationHelper.PaginateAsync(query, page, pageSize);

        foreach (var invoice in paginatedResult.Data)
        {
            // Fetch the BRN dynamically from APVEN table
            var supplier = await dbContext.AccountPayableVendors
                .FirstOrDefaultAsync(c => c.VENDORID == invoice.VDCODE.Trim());
            invoice.SupplierBRN = supplier?.BRN;

            trimStringService.TrimStringProperties(invoice);
            invoice.PurchaseInvoiceDetails = invoice.PurchaseInvoiceDetails
                .Select(detail => trimStringService.TrimStringProperties(detail))
                .ToList();
        }
        return paginatedResult;
    }

    /// <summary>
    /// Retrieves purchase credit/debit notes (self-billing).
    /// PO => POCRNH1 (Purchase Credit/Debit Note Header) and POCRNL (Purchase Credit/Debit Note Line)
    /// AP => APIBH (Accounts Payable Invoice Header) and APIBD (Accounts Payable Invoice Detail)
    /// </summary>
    public async Task<PaginatedResult<PurchaseCreditDebitNoteHeader>> GetPurchaseCreditDebitNotes(
        int page = 1,
        int pageSize = 10,
        string? noteNumber = null)
    {
        var query = dbContext.PurchaseCreditNoteHeaders
                .Include(h => h.PurchaseCreditDebitNoteDetails)
                .AsQueryable();

        if (!string.IsNullOrEmpty(noteNumber))
        {
            query = query.Where(h => EF.Functions.Like(h.CRNNUMBER, $"%{noteNumber}%"));
        }

        var paginatedResult = await PaginationHelper.PaginateAsync(query, page, pageSize);

        foreach (var note in paginatedResult.Data)
        {
            // Fetch the BRN dynamically from APVEN table
            var supplier = await dbContext.AccountPayableVendors
                .FirstOrDefaultAsync(c => c.VENDORID == note.VDCODE.Trim());
            note.SupplierBRN = supplier?.BRN;

            trimStringService.TrimStringProperties(note);
            note.PurchaseCreditDebitNoteDetails = note.PurchaseCreditDebitNoteDetails
                .Select(detail => trimStringService.TrimStringProperties(detail))
                .ToList();
        }

        return paginatedResult;
    }
}
