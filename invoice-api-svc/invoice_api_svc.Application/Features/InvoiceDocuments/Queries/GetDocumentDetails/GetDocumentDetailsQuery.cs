using invoice_api_svc.Application.DTOs.EInvoice.Document;
using invoice_api_svc.Application.Exceptions;
using invoice_api_svc.Application.Interfaces.Apis;
using invoice_api_svc.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.InvoiceDocuments.Queries.GetDocumentDetails
{
    public partial class GetDocumentDetailsQuery : IRequest<Response<DocumentDetails>>
    {
        public string Uuid { get; set; }
    }

    public class GetDocumentDetailsQueryHandler : IRequestHandler<GetDocumentDetailsQuery, Response<DocumentDetails>>
    {
        private readonly ILhdnApi _lhdnApi;

        public GetDocumentDetailsQueryHandler(ILhdnApi lhdnApi)
        {
            _lhdnApi = lhdnApi;
        }

        public async Task<Response<DocumentDetails>> Handle(GetDocumentDetailsQuery request, CancellationToken cancellationToken)
        {
            var documentDetails = await _lhdnApi.GetDocumentDetailsAsync(request.Uuid);

            if (documentDetails == null)
            {
                throw new ApiException("Failed to get document details.");
            }

            return new Response<DocumentDetails>(documentDetails);
        }
    }
}
