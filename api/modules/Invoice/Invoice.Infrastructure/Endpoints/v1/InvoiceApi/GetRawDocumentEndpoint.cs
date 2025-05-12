using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GetRawDocument.v1;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Document;
using NexKoala.Framework.Infrastructure.Identity.Users;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;

public static class GetRawDocumentEndpoint
{
    internal static RouteHandlerBuilder MapGetRawDocumentEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/{uuid}/raw",
                async (ISender mediator, string uuid, HttpContext context) =>
                {
                    if (context.User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
                    {
                        return Results.Unauthorized();
                    }
                    bool isAdmin = false;
                    if (context.User.IsInRole("Admin"))
                    {
                        isAdmin = true;
                    }
                    var response = await mediator.Send(new GetRawDocument(uuid, userId, isAdmin));
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
