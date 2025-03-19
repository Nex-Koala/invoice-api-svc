using System.Reflection;
using Asp.Versioning.Conventions;
using FluentValidation;
using NexKoala.Framework.Core;
using NexKoala.Framework.Core.Origin;
using NexKoala.Framework.Infrastructure.Auth;
using NexKoala.Framework.Infrastructure.Auth.Jwt;
using NexKoala.Framework.Infrastructure.Behaviours;
using NexKoala.Framework.Infrastructure.Caching;
using NexKoala.Framework.Infrastructure.Cors;
using NexKoala.Framework.Infrastructure.Exceptions;
using NexKoala.Framework.Infrastructure.Identity;
using NexKoala.Framework.Infrastructure.Jobs;
using NexKoala.Framework.Infrastructure.Logging.Serilog;
using NexKoala.Framework.Infrastructure.Mail;
using NexKoala.Framework.Infrastructure.OpenApi;
using NexKoala.Framework.Infrastructure.Persistence;
using NexKoala.Framework.Infrastructure.RateLimit;
using NexKoala.Framework.Infrastructure.SecurityHeaders;
using NexKoala.Framework.Infrastructure.Storage.Files;
using NexKoala.Framework.Infrastructure.Tenant;
using NexKoala.Framework.Infrastructure.Tenant.Endpoints;
using NexKoala.Aspire.ServiceDefaults;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace NexKoala.Framework.Infrastructure;

public static class Extensions
{
    public static WebApplicationBuilder ConfigureNexKoalaFramework(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.AddServiceDefaults();
        builder.ConfigureSerilog();
        builder.ConfigureDatabase();
        builder.Services.ConfigureMultitenancy();
        builder.Services.ConfigureIdentity();
        builder.Services.AddCorsPolicy(builder.Configuration);
        builder.Services.ConfigureFileStorage();
        builder.Services.ConfigureJwtAuth();
        builder.Services.ConfigureOpenApi();
        builder.Services.ConfigureJobs(builder.Configuration);
        builder.Services.ConfigureMailing();
        builder.Services.ConfigureCaching(builder.Configuration);
        builder.Services.AddExceptionHandler<CustomExceptionHandler>();
        builder.Services.AddProblemDetails();
        builder.Services.AddHealthChecks();
        builder.Services.AddOptions<OriginOptions>().BindConfiguration(nameof(OriginOptions));

        // Define module assemblies
        var assemblies = new Assembly[]
        {
            typeof(Core.Core).Assembly,
            typeof(Infrastructure).Assembly
        };

        // Register validators
        builder.Services.AddValidatorsFromAssemblies(assemblies);

        // Register MediatR
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(assemblies);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        builder.Services.ConfigureRateLimit(builder.Configuration);
        builder.Services.ConfigureSecurityHeaders(builder.Configuration);

        return builder;
    }

    public static WebApplication UseNexKoalaFramework(this WebApplication app)
    {
        app.MapDefaultEndpoints();
        app.UseRateLimit();
        app.UseSecurityHeaders();
        app.UseMultitenancy();
        app.UseExceptionHandler();
        app.UseCorsPolicy();
        app.UseOpenApi();
        app.UseJobDashboard(app.Configuration);
        app.UseRouting();
        app.UseStaticFiles();
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "assets")),
            RequestPath = new PathString("/assets")
        });
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapTenantEndpoints();
        app.MapIdentityEndpoints();

        // Current user middleware
        app.UseMiddleware<CurrentUserMiddleware>();

        // Register API versions
        var versions = app.NewApiVersionSet()
                    .HasApiVersion(1)
                    .HasApiVersion(2)
                    .ReportApiVersions()
                    .Build();

        // Map versioned endpoint
        app.MapGroup("api/v{version:apiVersion}").WithApiVersionSet(versions);

        return app;
    }
}
