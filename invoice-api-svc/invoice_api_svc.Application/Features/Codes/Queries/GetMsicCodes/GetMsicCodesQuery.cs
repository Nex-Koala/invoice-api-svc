using invoice_api_svc.Application.DTOs.EInvoice.Code;
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

namespace invoice_api_svc.Application.Features.Codes.Queries.GetMsicCodes
{
    public partial class GetMsicCodesQuery : IRequest<Response<List<MsicSubCategoryCode>>>
    {
    }

    public class GetMsicCodesQueryHandler : IRequestHandler<GetMsicCodesQuery, Response<List<MsicSubCategoryCode>>>
    {
        private readonly ILhdnSdk _lhdnSdk;

        public GetMsicCodesQueryHandler(ILhdnSdk lhdnSdk)
        {
            _lhdnSdk = lhdnSdk;
        }

        public async Task<Response<List<MsicSubCategoryCode>>> Handle(GetMsicCodesQuery request, CancellationToken cancellationToken)
        {
            var response = await _lhdnSdk.GetMsicCodesAsync();
            if (response == null)
            {
                throw new ApiException("Failed to get MSIC code.");
            }

            return new Response<List<MsicSubCategoryCode>>(response);
        }
    }
}
