using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;
using ClosedXML.Excel;
using iText.Html2pdf;
using Microsoft.EntityFrameworkCore;
using NexKoala.WebApi.Invoice.Application.Dtos;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Get.v1;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Domain.Entities.OE;
using NexKoala.WebApi.Invoice.Domain.Entities.PO;
using NexKoala.WebApi.Invoice.Infrastructure.Helpers;
using NexKoala.WebApi.Invoice.Infrastructure.Persistence;
using QRCoder;

namespace NexKoala.WebApi.Invoice.Infrastructure.Services;
public class InvoiceService(ClientDbContext dbContext, TrimStringService trimStringService, InvoiceDbContext invoiceDbContext) : IInvoiceService
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
    public async Task<PaginatedResult<OrderEntryHeader>> GetSalesInvoices(InvoiceFilterParams filter)
    {
        var submittedInvoiceNumbers = new HashSet<string>(
            await invoiceDbContext.InvoiceDocuments
                .Where(x => x.DocumentStatus == Domain.Entities.DocumentStatus.Submitted
                            || x.DocumentStatus == Domain.Entities.DocumentStatus.Valid)
                .Select(x => x.InvoiceNumber)
                .Distinct()
                .ToListAsync()
        );

        var query = dbContext.OrderEntryHeaders
                    .Include(h => h.OrderEntryDetails)
                    .AsQueryable();

        if (!string.IsNullOrEmpty(filter.InvoiceNumber))
        {
            query = query.Where(h => EF.Functions.Like(h.INVNUMBER, $"%{filter.InvoiceNumber}%"));
        }

        if (!string.IsNullOrWhiteSpace(filter.BuyerName))
        {
            query = query.Where(h => EF.Functions.Like(h.BILNAME, $"%{filter.BuyerName}%"));
        }

        if (filter.InvoiceDateFrom.HasValue && filter.InvoiceDateTo.HasValue)
        {
            query = query.Where(h =>
                h.INVDATE >= filter.InvoiceDateFrom.Value &&
                h.INVDATE <= filter.InvoiceDateTo.Value);
        }

        query = query.Where(h => !submittedInvoiceNumbers.Contains(h.INVNUMBER));

        var paginatedResult = await PaginationHelper.PaginateAsync(query, filter.Page, filter.PageSize);

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
    public async Task<PaginatedResult<OrderCreditDebitHeader>> GetCreditDebitNotes(InvoiceFilterParams filter)
    {
        var submittedInvoiceNumbers = new HashSet<string>(
            await invoiceDbContext.InvoiceDocuments
                .Where(x => x.DocumentStatus == Domain.Entities.DocumentStatus.Submitted
                            || x.DocumentStatus == Domain.Entities.DocumentStatus.Valid)
                .Select(x => x.InvoiceNumber)
                .Distinct()
                .ToListAsync()
        );

        var query = dbContext.OrderCreditDebitHeaders
                .Include(h => h.OrderCreditDebitDetails)
                .AsQueryable();

        if (!string.IsNullOrEmpty(filter.InvoiceNumber))
        {
            query = query.Where(h => EF.Functions.Like(h.INVNUMBER, $"%{filter.InvoiceNumber}%"));
        }

        if (!string.IsNullOrWhiteSpace(filter.BuyerName))
        {
            query = query.Where(h => EF.Functions.Like(h.BILNAME, $"%{filter.BuyerName}%"));
        }

        if (filter.InvoiceDateFrom.HasValue && filter.InvoiceDateTo.HasValue)
        {
            query = query.Where(h =>
                h.CRDDATE >= filter.InvoiceDateFrom.Value &&
                h.CRDDATE <= filter.InvoiceDateTo.Value);
        }

        query = query.Where(h => !submittedInvoiceNumbers.Contains(h.INVNUMBER));

        var paginatedResult = await PaginationHelper.PaginateAsync(query, filter.Page, filter.PageSize);

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
    public async Task<PaginatedResult<PurchaseInvoiceHeader>> GetPurchaseInvoices(InvoiceFilterParams filter)
    {
        var submittedInvoiceNumbers = new HashSet<string>(
            await invoiceDbContext.InvoiceDocuments
                .Where(x => x.DocumentStatus == Domain.Entities.DocumentStatus.Submitted
                            || x.DocumentStatus == Domain.Entities.DocumentStatus.Valid)
                .Select(x => x.InvoiceNumber)
                .Distinct()
                .ToListAsync()
        );

        var query = dbContext.PurchaseInvoiceHeaders
                .Include(h => h.PurchaseInvoiceDetails)
                .AsQueryable();

        if (!string.IsNullOrEmpty(filter.InvoiceNumber))
        {
            query = query.Where(h => EF.Functions.Like(h.INVNUMBER, $"%{filter.InvoiceNumber}%"));
        }

        if (!string.IsNullOrWhiteSpace(filter.SupplierName))
        {
            query = query.Where(h => EF.Functions.Like(h.VDNAME, $"%{filter.SupplierName}%"));
        }

        if (filter.InvoiceDateFrom.HasValue && filter.InvoiceDateTo.HasValue)
        {
            query = query.Where(h =>
                h.DATE >= filter.InvoiceDateFrom.Value &&
                h.DATE <= filter.InvoiceDateTo.Value);
        }

        query = query.Where(h => !submittedInvoiceNumbers.Contains(h.INVNUMBER));

        var paginatedResult = await PaginationHelper.PaginateAsync(query, filter.Page, filter.PageSize);

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
    public async Task<PaginatedResult<PurchaseCreditDebitNoteHeader>> GetPurchaseCreditDebitNotes(InvoiceFilterParams filter)
    {
        var submittedInvoiceNumbers = new HashSet<string>(
            await invoiceDbContext.InvoiceDocuments
                .Where(x => x.DocumentStatus == Domain.Entities.DocumentStatus.Submitted
                            || x.DocumentStatus == Domain.Entities.DocumentStatus.Valid)
                .Select(x => x.InvoiceNumber)
                .Distinct()
                .ToListAsync()
        );

        var query = dbContext.PurchaseCreditNoteHeaders
                .Include(h => h.PurchaseCreditDebitNoteDetails)
                .AsQueryable();

        if (!string.IsNullOrEmpty(filter.NoteNumber))
        {
            query = query.Where(h => EF.Functions.Like(h.CRNNUMBER, $"%{filter.NoteNumber}%"));
        }

        if (!string.IsNullOrEmpty(filter.InvoiceNumber))
        {
            query = query.Where(h => EF.Functions.Like(h.INVNUMBER, $"%{filter.InvoiceNumber}%"));
        }

        if (!string.IsNullOrWhiteSpace(filter.SupplierName))
        {
            query = query.Where(h => EF.Functions.Like(h.VDNAME, $"%{filter.SupplierName}%"));
        }

        if (filter.InvoiceDateFrom.HasValue && filter.InvoiceDateTo.HasValue)
        {
            query = query.Where(h =>
                h.DATE >= filter.InvoiceDateFrom.Value &&
                h.DATE <= filter.InvoiceDateTo.Value);
        }

        query = query.Where(h => !submittedInvoiceNumbers.Contains(h.INVNUMBER));

        var paginatedResult = await PaginationHelper.PaginateAsync(query, filter.Page, filter.PageSize);

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

    public byte[] ExportInvoiceSubmissionExcel(IEnumerable<InvoiceDocumentResponse> documents)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Invoice Submissions");

        // Header
        var headers = new[]
        {
            "Invoice No", "UUID", "Issue Date", "Supplier Name", "Supplier TIN", "Customer Name",
            "Total Payable Amount", "Tax Amount", "Total Excl. Tax", "Total Incl. Tax",
            "Currency", "Status"
        };

        for (int i = 0; i < headers.Length; i++)
            worksheet.Cell(1, i + 1).Value = headers[i];

        // Data
        int row = 2;
        foreach (var doc in documents)
        {
            worksheet.Cell(row, 1).Value = doc.InvoiceNumber;
            worksheet.Cell(row, 2).Value = doc.Uuid;
            worksheet.Cell(row, 3).Value = doc.IssueDate.ToString("yyyy-MM-dd");
            worksheet.Cell(row, 4).Value = doc.Supplier?.Name ?? "-";
            worksheet.Cell(row, 5).Value = doc.Supplier?.Tin ?? "-";
            worksheet.Cell(row, 6).Value = doc.Customer?.Name ?? "-";
            worksheet.Cell(row, 7).Value = doc.TotalAmount;
            worksheet.Cell(row, 8).Value = doc.TaxAmount;
            worksheet.Cell(row, 9).Value = doc.TotalExcludingTax;
            worksheet.Cell(row, 10).Value = doc.TotalIncludingTax;
            worksheet.Cell(row, 11).Value = doc.DocumentCurrencyCode ?? "-";
            worksheet.Cell(row, 12).Value = doc.DocumentStatus?.ToString() ?? "-";
            row++;
        }

        // Optional: Auto adjust column widths
        worksheet.Columns().AdjustToContents();

        // Save to byte array
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var excelData = stream.ToArray();

        return excelData;
    }
}
