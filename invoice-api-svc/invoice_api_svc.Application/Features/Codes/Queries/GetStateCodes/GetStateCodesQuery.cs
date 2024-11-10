using invoice_api_svc.Application.DTOs.EInvoice.Code;
using invoice_api_svc.Application.Exceptions;
using invoice_api_svc.Application.Interfaces.Apis;
using invoice_api_svc.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.Codes.Queries.GetStateCodes
{
    public partial class GetStateCodesQuery : IRequest<Response<List<StateCode>>>
    {
    }

    public class GetStateCodesQueryHandler : IRequestHandler<GetStateCodesQuery, Response<List<StateCode>>>
    {
        private readonly ILhdnSdk _lhdnSdk;

        public GetStateCodesQueryHandler(ILhdnSdk lhdnSdk)
        {
            _lhdnSdk = lhdnSdk;
        }

        public async Task<Response<List<StateCode>>> Handle(GetStateCodesQuery request, CancellationToken cancellationToken)
        {
            var response = await _lhdnSdk.GetStateCodesAsync();
            if (response == null)
            {
                throw new ApiException("Failed to get state codes.");
            }

            return new Response<List<StateCode>>(response);
        }
    }
}
