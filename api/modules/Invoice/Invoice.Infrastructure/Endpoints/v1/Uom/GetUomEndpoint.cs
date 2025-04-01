using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.Uoms.Get.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Uom;

public static class GetUomEndpoint
{
    internal static RouteHandlerBuilder MapGetUomEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/{id:guid}",
                async (Guid id, ISender mediator) =>
                {
                    var response = await mediator.Send(new GetUomRequest(id));
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetUomEndpoint))
            .WithSummary("gets uom by id")
            .WithDescription("gets prodct by id")
            .Produces<UomResponse>()
            .RequirePermission("Permissions.Uoms.View")
            .MapToApiVersion(1);
    }
}
