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

namespace invoice_api_svc.Application.Features.Codes.Queries.GetClassificationCodes
{
    public partial class GetClassificationCodesQuery : IRequest<Response<List<ClassificationCode>>>
    {
    }

    public class GetClassificationCodesQueryHandler : IRequestHandler<GetClassificationCodesQuery, Response<List<ClassificationCode>>>
    {
        private readonly ILhdnSdk _lhdnSdk;

        public GetClassificationCodesQueryHandler(ILhdnSdk lhdnSdk)
        {
            _lhdnSdk = lhdnSdk;
        }

        public async Task<Response<List<ClassificationCode>>> Handle(GetClassificationCodesQuery request, CancellationToken cancellationToken)
        {
            var response = await _lhdnSdk.GetClassificationCodesAsync();
            if (response == null)
            {
                throw new ApiException("Failed to get classification code.");
            }

            return new Response<List<ClassificationCode>>(response);
        }
    }
}
