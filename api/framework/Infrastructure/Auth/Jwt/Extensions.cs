﻿using NexKoala.Framework.Infrastructure.Auth.Policy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NexKoala.Framework.Core.Auth.Jwt;

namespace NexKoala.Framework.Infrastructure.Auth.Jwt;
internal static class Extensions
{
    internal static IServiceCollection ConfigureJwtAuth(this IServiceCollection services)
    {
        services.AddOptions<JwtOptions>()
            .BindConfiguration(nameof(JwtOptions))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();
        services
            .AddAuthentication(authentication =>
            {
                authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, null!);

        services.AddAuthorizationBuilder().AddRequiredPermissionPolicy();
        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = options.GetPolicy(RequiredPermissionDefaults.PolicyName);
        });
        return services;
    }
}
