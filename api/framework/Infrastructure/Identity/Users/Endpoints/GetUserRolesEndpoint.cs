﻿using NexKoala.Framework.Infrastructure.Auth.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Identity.Users.Abstractions;

namespace NexKoala.Framework.Infrastructure.Identity.Users.Endpoints;
public static class GetUserRolesEndpoint
{
    internal static RouteHandlerBuilder MapGetUserRolesEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/{id:guid}/roles", (string id, IUserService service) =>
        {
            return service.GetUserRolesAsync(id, CancellationToken.None);
        })
        .WithName(nameof(GetUserRolesEndpoint))
        .WithSummary("get user roles")
        .RequirePermission("Permissions.Users.View")
        .WithDescription("get user roles");
    }
}
