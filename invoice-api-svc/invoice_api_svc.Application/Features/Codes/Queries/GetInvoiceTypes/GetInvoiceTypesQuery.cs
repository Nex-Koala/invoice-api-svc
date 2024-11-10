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

namespace invoice_api_svc.Application.Features.Codes.Queries.GetInvoiceTypes
{
    public partial class GetInvoiceTypesQuery : IRequest<Response<List<EInvoiceType>>>
    {
    }

    public class GetInvoiceTypesQueryHandler : IRequestHandler<GetInvoiceTypesQuery, Response<List<EInvoiceType>>>
    {
        private readonly ILhdnSdk _lhdnSdk;

        public GetInvoiceTypesQueryHandler(ILhdnSdk lhdnSdk)
        {
            _lhdnSdk = lhdnSdk;
        }

        public async Task<Response<List<EInvoiceType>>> Handle(GetInvoiceTypesQuery request, CancellationToken cancellationToken)
        {
            var response = await _lhdnSdk.GetInvoiceTypesAsync();
            if (response == null)
            {
                throw new ApiException("Failed to get invoice types.");
            }

            return new Response<List<EInvoiceType>>(response);
        }
    }
}
