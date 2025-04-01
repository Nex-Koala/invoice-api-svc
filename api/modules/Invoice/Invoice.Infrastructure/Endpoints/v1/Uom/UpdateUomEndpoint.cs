using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.Uoms.Update.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Uom;

public static class UpdateUomEndpoint
{
    internal static RouteHandlerBuilder MapUomUpdateEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPut(
                "/{id:guid}",
                async (Guid id, UpdateUomCommand request, ISender mediator) =>
                {
                    if (id != request.Id)
                        return Results.BadRequest();
                    var response = await mediator.Send(request);
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(UpdateUomEndpoint))
            .WithSummary("update a uom")
            .WithDescription("update a uom")
            .Produces<UpdateUomResponse>()
            .RequirePermission("Permissions.Uoms.Update")
            .MapToApiVersion(1);
    }
}
