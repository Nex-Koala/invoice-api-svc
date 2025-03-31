using MediatR;
using NexKoala.Framework.Core.Exceptions;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Document;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GetRawDocument.v1;

public sealed class GetRawDocumentHandler(
    ILhdnApi lhdnApi
) : IRequestHandler<GetRawDocument, Response<RawDocument>>
{
    public async Task<Response<RawDocument>> Handle(
        GetRawDocument request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await lhdnApi.GetDocumentAsync(request.Uuid);

        if (item == null)
            throw new GenericException("Failed to get document.");

        return new Response<RawDocument>(item);
    }
}
