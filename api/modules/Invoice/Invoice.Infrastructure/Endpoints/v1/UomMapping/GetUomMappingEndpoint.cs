using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.UomMappings.Get.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.UomMapping;

public static class GetUomMappingEndpoint
{
    internal static RouteHandlerBuilder MapGetUomMappingEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet(
                "/{id:guid}",
                async (Guid id, ISender mediator) =>
                {
                    var response = await mediator.Send(new GetUomMappingRequest(id));
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(GetUomMappingEndpoint))
            .WithSummary("gets uom mapping by id")
            .WithDescription("gets uom mapping by id")
            .Produces<UomMappingResponse>()
            .RequirePermission("Permissions.UomMappings.View")
            .MapToApiVersion(1);
    }
}
