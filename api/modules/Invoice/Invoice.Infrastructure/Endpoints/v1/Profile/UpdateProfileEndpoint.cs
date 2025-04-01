using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using NexKoala.WebApi.Invoice.Application.Features.Partners.Update.v1;

namespace NexKoala.WebApi.Invoice.Infrastructure.Endpoints.v1.Profile;

public static class UpdateProfileEndpoint
{
    internal static RouteHandlerBuilder MapUpdateProfileEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPut(
                "/{id}",
                async (Guid id, [FromBody] UpdateProfileCommand request, ISender mediator) =>
                {
                    if (id != request.Id)
                        return Results.BadRequest();
                    var response = await mediator.Send(request);
                    return Results.Ok(response);
                }
            )
            .WithName(nameof(UpdateProfileEndpoint))
            .WithSummary("update a profile")
            .WithDescription("update a profile")
            .Produces<Response<Guid>>()
            .RequirePermission("Permissions.Profile.Update")
            .MapToApiVersion(1);
    }
}
