using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexKoala.WebApi.Invoice.Application.Dtos;
using NexKoala.WebApi.Invoice.Domain.Entities.OE;
using NexKoala.WebApi.Invoice.Domain.Entities.PO;

namespace NexKoala.WebApi.Invoice.Application.Interfaces;

public interface IInvoiceService
{
    byte[] GenerateInvoice(string xmlContent, string xsltTemplatePath);

    string GenerateQRCode(string data);

    Task<PaginatedResult<OrderEntryHeader>> GetSalesInvoices(
        int page = 1,
        int pageSize = 10,
        string? invoiceNumber = null
    );

    Task<PaginatedResult<OrderCreditDebitHeader>> GetCreditDebitNotes(
        int page = 1,
        int pageSize = 10,
        string? sequenceNumber = null
    );

    Task<PaginatedResult<PurchaseInvoiceHeader>> GetPurchaseInvoices(
        int page = 1,
        int pageSize = 10,
        string? invoiceNumber = null
    );

    Task<PaginatedResult<PurchaseCreditDebitNoteHeader>> GetPurchaseCreditDebitNotes(
        int page = 1,
        int pageSize = 10,
        string? noteNumber = null
    );
}
