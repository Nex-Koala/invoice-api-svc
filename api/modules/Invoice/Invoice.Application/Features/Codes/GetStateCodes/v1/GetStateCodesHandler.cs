using MediatR;
using NexKoala.Framework.Core.Exceptions;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.Codes.GetStateCodes.v1;

public sealed class GetStateCodesHandler(
    ILhdnSdk lhdnSdk
) : IRequestHandler<GetStateCodes, Response<List<StateCode>>>
{
    public async Task<Response<List<StateCode>>> Handle(
        GetStateCodes request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await lhdnSdk.GetStateCodesAsync();

        if (item == null)
            throw new GenericException("Failed to get state code.");

        return new Response<List<StateCode>>(item);
    }
}
