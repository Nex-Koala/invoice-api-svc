using invoice_api_svc.Application.DTOs.EInvoice.Code;
using invoice_api_svc.Application.Exceptions;
using invoice_api_svc.Application.Interfaces.Apis;
using invoice_api_svc.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.Features.Codes.Queries.GetTaxTypes
{
    public partial class GetTaxTypesQuery : IRequest<Response<List<TaxType>>>
    {
    }

    public class GetTaxTypesQueryHandler : IRequestHandler<GetTaxTypesQuery, Response<List<TaxType>>>
    {
        private readonly ILhdnSdk _lhdnSdk;

        public GetTaxTypesQueryHandler(ILhdnSdk lhdnSdk)
        {
            _lhdnSdk = lhdnSdk;
        }

        public async Task<Response<List<TaxType>>> Handle(GetTaxTypesQuery request, CancellationToken cancellationToken)
        {
            var response = await _lhdnSdk.GetTaxTypesAsync();
            if (response == null)
            {
                throw new ApiException("Failed to get tax types.");
            }

            return new Response<List<TaxType>>(response);
        }
    }
}
