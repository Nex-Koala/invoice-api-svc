using MediatR;
using NexKoala.Framework.Core.Exceptions;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.Codes.GetCurrencyCodes.v1;

public sealed class GetCurrencyCodesHandler(
    ILhdnSdk lhdnSdk
) : IRequestHandler<GetCurrencyCodes, Response<List<CurrencyCode>>>
{
    public async Task<Response<List<CurrencyCode>>> Handle(
        GetCurrencyCodes request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await lhdnSdk.GetCurrencyCodesAsync();

        if (item == null)
            throw new GenericException("Failed to get currency code.");

        return new Response<List<CurrencyCode>>(item);
    }
}
