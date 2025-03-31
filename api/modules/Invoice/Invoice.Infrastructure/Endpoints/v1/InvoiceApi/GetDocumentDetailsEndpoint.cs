using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GetDocumentDetails.v1;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Document;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;

public static class GetDocumentDetailsEndpoint
{
    internal static RouteHandlerBuilder MapGetDocumentDetailsEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/{uuid}/details",
                async (ISender mediator, string uuid) =>
                {
                    var response = await mediator.Send(new GetDocumentDetails(uuid));
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetDocumentDetailsEndpoint))
            .WithSummary("get document details")
            .WithDescription("get document details")
            .Produces<Response<DocumentDetails>>()
            .MapToApiVersion(1);
    }
}
