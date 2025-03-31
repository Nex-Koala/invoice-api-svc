using MediatR;
using NexKoala.Framework.Core.Exceptions;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.Codes.GetUnitTypes.v1;

public sealed class GetUnitTypesHandler(
    ILhdnSdk lhdnSdk
) : IRequestHandler<GetUnitTypes, Response<List<UnitType>>>
{
    public async Task<Response<List<UnitType>>> Handle(
        GetUnitTypes request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await lhdnSdk.GetUnitTypesAsync();

        if (item == null)
            throw new GenericException("Failed to get unit types.");

        return new Response<List<UnitType>>(item);
    }
}
