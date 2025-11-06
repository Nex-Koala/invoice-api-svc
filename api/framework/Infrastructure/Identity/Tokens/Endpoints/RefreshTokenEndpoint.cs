using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using NexKoala.Framework.Core.Auth.Jwt;
using NexKoala.Framework.Core.Exceptions;
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
        return endpoints.MapPost("/refresh", async (
            HttpContext context,
            [FromHeader(Name = TenantConstants.Identifier)] string tenant,
            ITokenService service,
            IOptions<JwtOptions> jwtOptions,
            CancellationToken cancellationToken) =>
        {
            string ip = context.GetIpAddress();

            var refreshToken = context.Request.Cookies["refresh_token"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new GenericException("Refresh token missing or expired.", [], System.Net.HttpStatusCode.Unauthorized);
            }

            var accessToken = context.Request.Cookies["access_token"];

            var newTokens = await service.RefreshTokenAsync(
                new RefreshTokenCommand(accessToken, refreshToken),
                ip!,
                cancellationToken
            );

            var accessCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Path = "/",
                Expires = DateTime.UtcNow.AddMinutes(jwtOptions.Value.TokenExpirationInMinutes)
            };

            var refreshCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Path = "/",
                Expires = DateTime.UtcNow.AddDays(jwtOptions.Value.RefreshTokenExpirationInDays)
            };

            context.Response.Cookies.Append("access_token", newTokens.Token, accessCookieOptions);
            context.Response.Cookies.Append("refresh_token", newTokens.RefreshToken, refreshCookieOptions);

            newTokens = newTokens with
            {
                Token = string.Empty,
                RefreshToken = string.Empty
            };

            return new Response<TokenResponse>(newTokens);
        })
        .WithName(nameof(RefreshTokenEndpoint))
        .WithSummary("Refresh JWTs and reissue cookies")
        .WithDescription("Refreshes JWT tokens securely via HttpOnly cookies")
        .AllowAnonymous();
    }
}
