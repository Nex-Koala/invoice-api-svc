using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.GetRecentDocuments.v1;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.RecentDocument;
using NexKoala.Framework.Infrastructure.Identity.Users;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.InvoiceApi;

public static class GetRecentDocumentsEndpoint
{
    internal static RouteHandlerBuilder MapGetRecentDocumentsEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/recent",
                async (ISender mediator, [AsParameters] GetRecentDocuments request, HttpContext context) =>
                {
                    if (context.User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
                    {
                        return Results.BadRequest();
                    }
                    request.UserId = userId;
                    var response = await mediator.Send(request);
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetRecentDocumentsEndpoint))
            .WithSummary("get recent documents")
            .WithDescription("get recent documents")
            .Produces<Response<RecentDocuments>>()
            .MapToApiVersion(1);
    }
}
