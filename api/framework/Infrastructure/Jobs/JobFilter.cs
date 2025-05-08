using Finbuckle.MultiTenant.Abstractions;
using NexKoala.Framework.Infrastructure.Constants;
using NexKoala.Framework.Infrastructure.Identity.Users;
using Hangfire.Client;
using Hangfire.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NexKoala.Framework.Core.Tenant;

namespace NexKoala.Framework.Infrastructure.Jobs;

public class JobFilter : IClientFilter
{
    private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

    private readonly IServiceProvider _services;

    public JobFilter(IServiceProvider services) => _services = services;

    public void OnCreating(CreatingContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        Logger.InfoFormat("Set TenantId and UserId parameters to job {0}.{1}...", context.Job.Method.ReflectedType?.FullName, context.Job.Method.Name);

        using var scope = _services.CreateScope();

        var httpContext = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext;

        // Safe fallback: Only set UserId if HttpContext is available
        if (httpContext != null)
        {
            string? userId = httpContext.User.GetUserId();
            context.SetJobParameter(QueryStringKeys.UserId, userId);
        }
        else
        {
            Logger.Warn("HttpContext is null. Skipping setting UserId.");
        }

        // TenantInfo should not depend on HttpContext if MultiTenantContext is available
        var tenantContext = scope.ServiceProvider.GetRequiredService<IMultiTenantContextAccessor>().MultiTenantContext;

        if (tenantContext?.TenantInfo != null)
        {
            context.SetJobParameter(TenantConstants.Identifier, tenantContext.TenantInfo);
        }
        else
        {
            Logger.Warn("TenantInfo is null. Skipping setting TenantId.");
        }
    }


    public void OnCreated(CreatedContext context) =>
        Logger.InfoFormat(
            "Job created with parameters {0}",
            context.Parameters.Select(x => x.Key + "=" + x.Value).Aggregate((s1, s2) => s1 + ";" + s2));
}
