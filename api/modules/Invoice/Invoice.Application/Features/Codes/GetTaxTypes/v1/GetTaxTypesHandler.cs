using MediatR;
using NexKoala.Framework.Core.Exceptions;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.Codes.GetTaxTypes.v1;

public sealed class GetTaxTypesHandler(
    ILhdnSdk lhdnSdk
) : IRequestHandler<GetTaxTypes, Response<List<TaxType>>>
{
    public async Task<Response<List<TaxType>>> Handle(
        GetTaxTypes request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await lhdnSdk.GetTaxTypesAsync();

        if (item == null)
            throw new GenericException("Failed to get tax types.");

        return new Response<List<TaxType>>(item);
    }
}
