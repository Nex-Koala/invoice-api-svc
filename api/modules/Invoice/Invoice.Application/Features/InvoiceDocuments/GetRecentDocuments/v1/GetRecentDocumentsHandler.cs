using MediatR;
using NexKoala.Framework.Core.Exceptions;
using NexKoala.WebApi.Invoice.Application.Interfaces;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.RecentDocument;

namespace NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GetRecentDocuments.v1;

public sealed class GetRecentDocumentsHandler(
    ILhdnApi lhdnApi
) : IRequestHandler<GetRecentDocuments, Response<RecentDocuments>>
{
    public async Task<Response<RecentDocuments>> Handle(
        GetRecentDocuments request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await lhdnApi.GetRecentDocumentsAsync(request);

        if (item == null)
            throw new GenericException("Failed to get document.");

        return new Response<RecentDocuments>(item);
    }
}
