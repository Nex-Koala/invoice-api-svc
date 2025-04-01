using MediatR;
using NexKoala.Framework.Core.Exceptions;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.Codes.GetInvoiceTypes.v1;

public sealed class GetInvoiceTypesHandler(
    ILhdnSdk lhdnSdk
) : IRequestHandler<GetInvoiceTypes, Response<List<EInvoiceType>>>
{
    public async Task<Response<List<EInvoiceType>>> Handle(
        GetInvoiceTypes request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await lhdnSdk.GetInvoiceTypesAsync();

        if (item == null)
            throw new GenericException("Failed to get invoice types.");

        return new Response<List<EInvoiceType>>(item);
    }
}
