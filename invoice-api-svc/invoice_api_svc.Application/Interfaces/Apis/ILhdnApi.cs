using invoice_api_svc.Application.DTOs.EInvoice.Document;
using invoice_api_svc.Application.DTOs.EInvoice.RecentDocument;
using invoice_api_svc.Application.DTOs.Ubl;
using invoice_api_svc.Application.Features.InvoiceDocuments.Queries.GetRecentDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Interfaces.Apis
{
    public interface ILhdnApi
    {
        Task<DocumentDetails> GetDocumentDetailsAsync(string uuid);
        Task<RecentDocuments> GetRecentDocumentsAsync(GetRecentDocumentsQuery request);
        Task<HttpResponseMessage> SubmitInvoiceAsync(UblDocumentRequest payload);
    }
}
