using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Document;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Invoice;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.RecentDocument;
using NexKoala.WebApi.Invoice.Application.Dtos.Ubl;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GetRecentDocuments.v1;

namespace NexKoala.WebApi.Invoice.Application.Interfaces;
public interface ILhdnApi
{
    Task<DocumentDetails> GetDocumentDetailsAsync(string uuid);
    Task<RecentDocuments> GetRecentDocumentsAsync(GetRecentDocuments request);
    Task<SubmitInvoiceResponse> SubmitInvoiceAsync(UblDocumentRequest payload);
    Task<RawDocument> GetDocumentAsync(string uuid);
}
