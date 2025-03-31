using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.Uoms.Delete.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Uom;

public static class DeleteUomEndpoint
{
    internal static RouteHandlerBuilder MapUomDeleteEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapDelete(
                "/{id:guid}",
                async (Guid id, ISender mediator) =>
                {
                    var response = await mediator.Send(new DeleteUomCommand(id));
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(DeleteUomEndpoint))
            .WithSummary("deletes uom by id")
            .WithDescription("deletes uom by id")
            .Produces<Response<Guid>>()
            .RequirePermission("Permissions.Uoms.Delete")
            .MapToApiVersion(1);
    }
}
