using MediatR;
using NexKoala.Framework.Core.Exceptions;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.Codes.GetMsicCodes.v1;

public sealed class GetMsicCodesHandler(
    ILhdnSdk lhdnSdk
) : IRequestHandler<GetMsicCodes, Response<List<MsicSubCategoryCode>>>
{
    public async Task<Response<List<MsicSubCategoryCode>>> Handle(
        GetMsicCodes request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await lhdnSdk.GetMsicCodesAsync();

        if (item == null)
            throw new GenericException("Failed to get MSIC code.");

        return new Response<List<MsicSubCategoryCode>>(item);
    }
}
