using Finbuckle.MultiTenant.Abstractions;

namespace NexKoala.Framework.Infrastructure.Tenant.Abstractions;
public interface IFshTenantInfo : ITenantInfo
{
    string? ConnectionString { get; set; }
}
