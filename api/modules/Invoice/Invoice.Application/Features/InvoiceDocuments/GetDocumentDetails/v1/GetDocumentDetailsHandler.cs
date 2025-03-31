using MediatR;
using NexKoala.Framework.Core.Exceptions;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Document;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GetDocumentDetails.v1;

public sealed class GetDocumentDetailsHandler(
    ILhdnApi lhdnApi
) : IRequestHandler<GetDocumentDetails, Response<DocumentDetails>>
{
    public async Task<Response<DocumentDetails>> Handle(
        GetDocumentDetails request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await lhdnApi.GetDocumentDetailsAsync(request.Uuid);

        if (item == null)
            throw new GenericException("Failed to get document details.");

        return new Response<DocumentDetails>(item);
    }
}
