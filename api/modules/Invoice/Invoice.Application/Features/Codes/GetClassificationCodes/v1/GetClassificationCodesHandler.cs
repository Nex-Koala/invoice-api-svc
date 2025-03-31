using MediatR;
using NexKoala.Framework.Core.Exceptions;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.Codes.GetClassificationCodes.v1;

public sealed class GetClassificationCodesHandler(
    ILhdnSdk lhdnSdk
) : IRequestHandler<GetClassificationCodes, Response<List<ClassificationCode>>>
{
    public async Task<Response<List<ClassificationCode>>> Handle(
        GetClassificationCodes request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await lhdnSdk.GetClassificationCodesAsync();

        if (item == null)
            throw new GenericException("Failed to get classification code.");

        return new Response<List<ClassificationCode>>(item);
    }
}
