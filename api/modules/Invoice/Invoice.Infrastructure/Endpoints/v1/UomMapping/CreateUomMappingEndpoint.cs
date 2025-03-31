using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.UomMappings.Create.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.UomMapping;

public static class CreateUomMappingEndpoint
{
    internal static RouteHandlerBuilder MapUomMappingCreationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost(
                "/",
                async (CreateUomMappingCommand request, ISender mediator) =>
                {
                    var response = await mediator.Send(request);
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(CreateUomMappingEndpoint))
            .WithSummary("creates a uom mapping")
            .WithDescription("creates a uom mapping")
            .Produces<Response<Guid>>()
            .RequirePermission("Permissions.UomMappings.Create")
            .MapToApiVersion(1);
    }
}
