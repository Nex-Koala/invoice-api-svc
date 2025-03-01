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

namespace invoice_api_svc.Application.Features.InvoiceDocuments.Queries.GetRawDocument
{
    public partial class GetRawDocuemntQuery : IRequest<Response<RawDocument>>
    {
        public string Uuid { get; set; }
    }

    public class GetRawDocuemntQueryHandler : IRequestHandler<GetRawDocuemntQuery, Response<RawDocument>>
    {
        private readonly ILhdnApi _lhdnApi;

        public GetRawDocuemntQueryHandler(ILhdnApi lhdnApi)
        {
            _lhdnApi = lhdnApi;
        }

        public async Task<Response<RawDocument>> Handle(GetRawDocuemntQuery request, CancellationToken cancellationToken)
        {
            var document = await _lhdnApi.GetDocumentAsync(request.Uuid);

            if (document == null)
            {
                throw new ApiException("Failed to get document.");
            }

            return new Response<RawDocument>(document);
        }
    }
}
