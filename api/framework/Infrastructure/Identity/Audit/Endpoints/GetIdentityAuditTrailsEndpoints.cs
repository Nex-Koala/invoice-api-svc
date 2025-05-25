using NexKoala.Framework.Infrastructure.Auth.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Audit;

namespace NexKoala.Framework.Infrastructure.Identity.Audit.Endpoints;

public static class GetIdentityAuditTrailsEndpoints
{
    internal static RouteHandlerBuilder MapGetIdentityAuditTrailsEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/audit-trails", (IAuditService service, Guid? userId, DateTimeOffset? startDate, DateTimeOffset? endDate, int pageNumber = 1, int pageSize = 20) =>
        {
            return service.GetIdentityAuditTrailsAsync(pageNumber, pageSize, userId, startDate, endDate);
        })
        .WithName(nameof(GetIdentityAuditTrailsEndpoints))
        .WithSummary("Get all user's audit trail details")
        .RequirePermission("Permissions.AuditTrails.View")
        .WithDescription("Get all user's audit trail details.");
    }
}
