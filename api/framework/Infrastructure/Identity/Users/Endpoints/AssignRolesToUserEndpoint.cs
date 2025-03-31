using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Identity.Users.Features.AssignUserRole;
using NexKoala.Framework.Core.Identity.Users.Abstractions;

namespace NexKoala.Framework.Infrastructure.Identity.Users.Endpoints;
public static class AssignRolesToUserEndpoint
{
    internal static RouteHandlerBuilder MapAssignRolesToUserEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/{id:guid}/roles", async (AssignUserRoleCommand command,
            HttpContext context,
            string id,
            IUserService userService,
            CancellationToken cancellationToken) =>
        {

            var message = await userService.AssignRolesAsync(id, command, cancellationToken);
            return Results.Ok(message);
        })
        .WithName(nameof(AssignRolesToUserEndpoint))
        .WithSummary("assign roles")
        .WithDescription("assign roles");
    }

}
