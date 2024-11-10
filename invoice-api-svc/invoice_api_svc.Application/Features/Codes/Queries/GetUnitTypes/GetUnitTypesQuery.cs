using invoice_api_svc.Application.DTOs.EInvoice.Code;
using invoice_api_svc.Application.Exceptions;
using invoice_api_svc.Application.Interfaces.Apis;
using invoice_api_svc.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using invoice_api_svc.Application.Features.Codes.Queries.GetPaymentMethods;

namespace invoice_api_svc.Application.Features.Codes.Queries.GetUnitTypes
{
    public partial class GetUnitTypesQuery : IRequest<Response<List<UnitType>>>
    {
    }

    public class GetUnitTypesQueryHandler : IRequestHandler<GetUnitTypesQuery, Response<List<UnitType>>>
    {
        private readonly ILhdnSdk _lhdnSdk;

        public GetUnitTypesQueryHandler(ILhdnSdk lhdnSdk)
        {
            _lhdnSdk = lhdnSdk;
        }

        public async Task<Response<List<UnitType>>> Handle(GetUnitTypesQuery request, CancellationToken cancellationToken)
        {
            var response = await _lhdnSdk.GetUnitTypesAsync();
            if (response == null)
            {
                throw new ApiException("Failed to get unit types.");
            }

            return new Response<List<UnitType>>(response);
        }
    }
}
