﻿using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using NexKoala.Framework.Core.Tenant;
using NexKoala.Framework.Core.Identity.Users.Features.ForgotPassword;
using NexKoala.Framework.Core.Identity.Users.Abstractions;
using NexKoala.Framework.Core.Origin;

namespace NexKoala.Framework.Infrastructure.Identity.Users.Endpoints;

public static class ForgotPasswordEndpoint
{
    internal static RouteHandlerBuilder MapForgotPasswordEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/forgot-password", async (HttpRequest request, [FromHeader(Name = TenantConstants.Identifier)] string tenant, ForgotPasswordCommand command, IOptions<OriginOptions> settings, IValidator<ForgotPasswordCommand> validator, IUserService userService, CancellationToken cancellationToken) =>
        {
            ValidationResult result = await validator.ValidateAsync(command, cancellationToken);
            if (!result.IsValid)
            {
                return Results.ValidationProblem(result.ToDictionary());
            }

            // Obtain origin from appsettings
            var origin = settings.Value;

            if (origin?.FrontendUrl == null)
            {
                // Handle the case where Frontend Url is null
                return Results.BadRequest("Origin URL is not configured.");
            }

            await userService.ForgotPasswordAsync(command, origin.FrontendUrl.ToString(), cancellationToken);
            return Results.Ok("Password reset email sent.");
        })
        .WithName(nameof(ForgotPasswordEndpoint))
        .WithSummary("Forgot password")
        .WithDescription("Generates a password reset token and sends it via email.")
        .AllowAnonymous();
    }

}
