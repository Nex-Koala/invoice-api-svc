﻿using NexKoala.Framework.Core.Tenant.Features.ActivateTenant;
using NexKoala.Framework.Infrastructure.Auth.Policy;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace NexKoala.Framework.Infrastructure.Tenant.Endpoints;
public static class ActivateTenantEndpoint
{
    internal static RouteHandlerBuilder MapActivateTenantEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/{id}/activate", (ISender mediator, string id) => mediator.Send(new ActivateTenantCommand(id)))
                                .WithName(nameof(ActivateTenantEndpoint))
                                .WithSummary("activate tenant")
                                .RequirePermission("Permissions.Tenants.Update")
                                .WithDescription("activate tenant");
    }
}
