using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using NexKoala.Framework.Core.Auth.Jwt;
using NexKoala.Framework.Core.Identity.Tokens;
using NexKoala.Framework.Core.Identity.Tokens.Features.Generate;
using NexKoala.Framework.Core.Identity.Tokens.Models;
using NexKoala.Framework.Core.Tenant;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.Framework.Infrastructure.Identity.Tokens.Endpoints;
public static class TokenGenerationEndpoint
{
    internal static RouteHandlerBuilder MapTokenGenerationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/", async (
            HttpContext context,
            TokenGenerationCommand request,
            [FromHeader(Name = TenantConstants.Identifier)] string tenant,
            ITokenService service,
            IOptions<JwtOptions> jwtOptions,
            CancellationToken cancellationToken) =>
        {
            string ip = context.GetIpAddress();
            var tokenResponse = await service.GenerateTokenAsync(request, ip!, cancellationToken);

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

            context.Response.Cookies.Append("access_token", tokenResponse.Token, accessCookieOptions);
            context.Response.Cookies.Append("refresh_token", tokenResponse.RefreshToken, refreshCookieOptions);

            tokenResponse = tokenResponse with
            {
                Token = string.Empty,
                RefreshToken = string.Empty
            };

            return new Response<TokenResponse>(tokenResponse);
        })
        .WithName(nameof(TokenGenerationEndpoint))
        .WithSummary("Generate JWTs and set cookies")
        .WithDescription("Generate JWTs, issue them as HttpOnly cookies for secure auth")
        .AllowAnonymous();
    }
}
