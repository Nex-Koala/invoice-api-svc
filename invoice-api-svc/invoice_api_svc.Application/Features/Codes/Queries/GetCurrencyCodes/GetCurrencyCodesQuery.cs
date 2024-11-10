using invoice_api_svc.Application.DTOs.EInvoice.Code;
using invoice_api_svc.Application.Exceptions;
using invoice_api_svc.Application.Interfaces.Apis;
using invoice_api_svc.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.Codes.Queries.GetCurrencyCodes
{
    public partial class GetCurrencyCodesQuery : IRequest<Response<List<CurrencyCode>>>
    {
    }

    public class GetCurrencyCodesHandler : IRequestHandler<GetCurrencyCodesQuery, Response<List<CurrencyCode>>>
    {
        private readonly ILhdnSdk _lhdnSdk;

        public GetCurrencyCodesHandler(ILhdnSdk lhdnSdk)
        {
            _lhdnSdk = lhdnSdk;
        }

        public async Task<Response<List<CurrencyCode>>> Handle(GetCurrencyCodesQuery request, CancellationToken cancellationToken)
        {
            var response = await _lhdnSdk.GetCurrencyCodesAsync();
            if (response == null)
            {
                throw new ApiException("Failed to get currency code.");
            }

            return new Response<List<CurrencyCode>>(response);
        }
    }
}
