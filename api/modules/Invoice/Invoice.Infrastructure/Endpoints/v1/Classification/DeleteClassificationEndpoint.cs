using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.Classifications.Delete.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Classification;

public static class DeleteClassificationEndpoint
{
    internal static RouteHandlerBuilder MapClassificationDeleteEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapDelete(
                "/{id:guid}",
                async (Guid id, ISender mediator) =>
                {
                    var response = await mediator.Send(new DeleteClassificationCommand(id));
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(DeleteClassificationEndpoint))
            .WithSummary("deletes classification by id")
            .WithDescription("deletes classification by id")
            .Produces<Response<Guid>>()
            .RequirePermission("Permissions.Classifications.Delete")
            .MapToApiVersion(1);
    }
}
