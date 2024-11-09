using invoice_api_svc.Application.DTOs.EInvoice.Document;
using invoice_api_svc.Application.DTOs.EInvoice.RecentDocument;
using invoice_api_svc.Application.Exceptions;
using invoice_api_svc.Application.Features.InvoiceDocuments.Queries.GetDocumentDetails;
using invoice_api_svc.Application.Interfaces.Apis;
using invoice_api_svc.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.InvoiceDocuments.Queries.GetRecentDocuments
{
    public partial class GetRecentDocumentsQuery : IRequest<Response<RecentDocuments>>
    {
        public int PageNo { get; set; } = 1;  // Default value set to 1
        public int PageSize { get; set; } = 10;  // Default value set to 10

        public string? SubmissionDateFrom { get; set; }
        public string? SubmissionDateTo { get; set; }
        public string? IssueDateFrom { get; set; }
        public string? IssueDateTo { get; set; }
        public string? Direction { get; set; }
        public string? Status { get; set; }
        public string? DocumentType { get; set; }
        public string? ReceiverIdType { get; set; }
        public string? ReceiverId { get; set; }
        public string? ReceiverTin { get; set; }
        public string? IssuerTin { get; set; }
        public string? IssuerIdType { get; set; }
        public string? IssuerId { get; set; }
    }

    public class GetRecentDocumentsQueryHandler : IRequestHandler<GetRecentDocumentsQuery, Response<RecentDocuments>>
    {
        private readonly ILhdnApi _lhdnApi;

        public GetRecentDocumentsQueryHandler(ILhdnApi lhdnApi)
        {
            _lhdnApi = lhdnApi;
        }

        public async Task<Response<RecentDocuments>> Handle(GetRecentDocumentsQuery request, CancellationToken cancellationToken)
        {
            var recentDocuments = await _lhdnApi.GetRecentDocumentsAsync(request);
            if (recentDocuments == null)
            {
                throw new ApiException("Failed to get recent documents.");
            }

            return new Response<RecentDocuments>(recentDocuments);
        }
    }
}
