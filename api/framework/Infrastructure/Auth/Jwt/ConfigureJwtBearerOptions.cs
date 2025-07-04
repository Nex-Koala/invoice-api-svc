﻿using System.Security.Claims;
using System.Text;
using NexKoala.Framework.Core.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NexKoala.Framework.Core.Auth.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace NexKoala.Framework.Infrastructure.Auth.Jwt;
public class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtOptions _options;

    public ConfigureJwtBearerOptions(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public void Configure(JwtBearerOptions options)
    {
        Configure(string.Empty, options);
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        if (name != JwtBearerDefaults.AuthenticationScheme)
        {
            return;
        }

        byte[] key = Encoding.ASCII.GetBytes(_options.Key);

        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidIssuer = JwtAuthConstants.Issuer,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidAudience = JwtAuthConstants.Audience,
            ValidateAudience = true,
            RoleClaimType = ClaimTypes.Role,
            ClockSkew = TimeSpan.Zero
        };
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                var endpoint = context.HttpContext.GetEndpoint();
                if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null)
                {
                    // Skip challenge for anonymous endpoints
                    return Task.CompletedTask;
                }

                context.HandleResponse();
                throw new UnauthorizedException();
            },
            OnForbidden = _ => throw new ForbiddenException(),
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                if (!string.IsNullOrEmpty(accessToken) &&
                    context.HttpContext.Request.Path.StartsWithSegments("/notifications", StringComparison.OrdinalIgnoreCase))
                {
                    // Read the token out of the query string
                    context.Token = accessToken;
                }

                return Task.CompletedTask;
            }
        };
    }
}
