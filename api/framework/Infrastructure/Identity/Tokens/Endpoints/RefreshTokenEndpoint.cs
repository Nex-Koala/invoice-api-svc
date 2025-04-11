using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NexKoala.Framework.Core.Identity.Tokens;
using NexKoala.Framework.Core.Identity.Tokens.Features.Refresh;
using NexKoala.Framework.Core.Identity.Tokens.Models;
using NexKoala.Framework.Core.Tenant;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.Framework.Infrastructure.Identity.Tokens.Endpoints;
public static class RefreshTokenEndpoint
{
    internal static RouteHandlerBuilder MapRefreshTokenEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/refresh", async (RefreshTokenCommand request,
            [FromHeader(Name = TenantConstants.Identifier)] string tenant,
            ITokenService service,
            HttpContext context,
            CancellationToken cancellationToken) =>
        {
            string ip = context.GetIpAddress();
            return new Response<TokenResponse>(await service.RefreshTokenAsync(request, ip!, cancellationToken));
        })
        .WithName(nameof(RefreshTokenEndpoint))
        .WithSummary("refresh JWTs")
        .WithDescription("refresh JWTs")
        .AllowAnonymous();
    }
}
