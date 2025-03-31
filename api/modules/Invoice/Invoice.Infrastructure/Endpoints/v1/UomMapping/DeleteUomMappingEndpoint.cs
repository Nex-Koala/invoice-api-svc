using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.UomMappings.Delete.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.UomMapping;

public static class DeleteUomMappingEndpoint
{
    internal static RouteHandlerBuilder MapUomMappingDeleteEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapDelete(
                "/{id:guid}",
                async (Guid id, ISender mediator) =>
                {
                    var response = await mediator.Send(new DeleteUomMappingCommand(id));
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(DeleteUomMappingEndpoint))
            .WithSummary("deletes uom mapping by id")
            .WithDescription("deletes uom mapping by id")
            .Produces<Response<Guid>>()
            .RequirePermission("Permissions.UomMappings.Delete")
            .MapToApiVersion(1);
    }
}
