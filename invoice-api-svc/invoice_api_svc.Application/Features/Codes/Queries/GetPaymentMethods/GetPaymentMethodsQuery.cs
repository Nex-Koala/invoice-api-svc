using invoice_api_svc.Application.DTOs.EInvoice.Code;
using invoice_api_svc.Application.Exceptions;
using invoice_api_svc.Application.Interfaces.Apis;
using invoice_api_svc.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.Codes.Queries.GetPaymentMethods
{
    public partial class GetPaymentMethodsQuery : IRequest<Response<List<PaymentMethodResponse>>>
    {
    }

    public class GetPaymentMethodsQueryHandler : IRequestHandler<GetPaymentMethodsQuery, Response<List<PaymentMethodResponse>>>
    {
        private readonly ILhdnSdk _lhdnSdk;

        public GetPaymentMethodsQueryHandler(ILhdnSdk lhdnSdk)
        {
            _lhdnSdk = lhdnSdk;
        }

        public async Task<Response<List<PaymentMethodResponse>>> Handle(GetPaymentMethodsQuery request, CancellationToken cancellationToken)
        {
            var response = await _lhdnSdk.GetPaymentMethodsAsync();
            if (response == null)
            {
                throw new ApiException("Failed to get payment methods.");
            }

            return new Response<List<PaymentMethodResponse>>(response);
        }
    }
}
