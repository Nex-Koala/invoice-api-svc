using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.ClassificationMappings.Delete.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.ClassificationMapping;

public static class DeleteClassificationMappingEndpoint
{
    internal static RouteHandlerBuilder MapClassificationMappingDeleteEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapDelete(
                "/{id:guid}",
                async (Guid id, ISender mediator) =>
                {
                    var response = await mediator.Send(new DeleteClassificationMappingCommand(id));
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(DeleteClassificationMappingEndpoint))
            .WithSummary("deletes classification mapping by id")
            .WithDescription("deletes classification mapping by id")
            .Produces<Response<Guid>>()
            .RequirePermission("Permissions.ClassificationMappings.Delete")
            .MapToApiVersion(1);
    }
}
