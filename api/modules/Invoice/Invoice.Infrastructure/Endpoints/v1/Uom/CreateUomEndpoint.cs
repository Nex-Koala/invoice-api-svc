using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.Uoms.Create.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Uom;

public static class CreateUomEndpoint
{
    internal static RouteHandlerBuilder MapUomCreationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost(
                "/",
                async (CreateUomCommand request, ISender mediator) =>
                {
                    var response = await mediator.Send(request);
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(CreateUomEndpoint))
            .WithSummary("creates a uom")
            .WithDescription("creates a uom")
            .Produces<Response<Guid>>()
            .RequirePermission("Permissions.Uoms.Create")
            .MapToApiVersion(1);
    }
}
