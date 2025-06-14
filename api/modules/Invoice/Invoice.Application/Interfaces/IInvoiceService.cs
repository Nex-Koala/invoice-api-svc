using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexKoala.WebApi.Invoice.Application.Dtos;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Get.v1;
using NexKoala.WebApi.Invoice.Domain.Entities.OE;
using NexKoala.WebApi.Invoice.Domain.Entities.PO;

namespace NexKoala.WebApi.Invoice.Application.Interfaces;

public interface IInvoiceService
{
    byte[] GenerateInvoice(string xmlContent, string xsltTemplatePath);

    string GenerateQRCode(string data);

    Task<PaginatedResult<OrderEntryHeader>> GetSalesInvoices(InvoiceFilterParams filter);

    Task<PaginatedResult<OrderCreditDebitHeader>> GetCreditDebitNotes(InvoiceFilterParams filter);

    Task<PaginatedResult<PurchaseInvoiceHeader>> GetPurchaseInvoices(InvoiceFilterParams filter);

    Task<PaginatedResult<PurchaseCreditDebitNoteHeader>> GetPurchaseCreditDebitNotes(InvoiceFilterParams filter);

    byte[] ExportInvoiceSubmissionExcel(IEnumerable<InvoiceDocumentResponse> documents);
}
