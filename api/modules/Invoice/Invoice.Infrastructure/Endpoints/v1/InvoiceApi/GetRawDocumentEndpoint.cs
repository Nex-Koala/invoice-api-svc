using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GetRawDocument.v1;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Document;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;

public static class GetRawDocumentEndpoint
{
    internal static RouteHandlerBuilder MapGetRawDocumentEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/{uuid}/raw",
                async (ISender mediator, string uuid) =>
                {
                    var response = await mediator.Send(new GetRawDocument(uuid));
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetRawDocumentEndpoint))
            .WithSummary("get raw document")
            .WithDescription("get raw document")
            //.Produces<Response<RawDocument>>()
            .MapToApiVersion(1);
    }
}
