using Finbuckle.MultiTenant.Abstractions;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using NexKoala.Framework.Core.Audit;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Infrastructure.Identity.RoleClaims;
using NexKoala.Framework.Infrastructure.Identity.Roles;
using NexKoala.Framework.Infrastructure.Identity.Users;
using NexKoala.Framework.Infrastructure.Persistence;
using NexKoala.Framework.Infrastructure.Tenant;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace NexKoala.Framework.Infrastructure.Identity.Persistence;
public class IdentityDbContext : MultiTenantIdentityDbContext<User,
    Role,
    string,
    IdentityUserClaim<string>,
    IdentityUserRole<string>,
    IdentityUserLogin<string>,
    RoleClaim,
    IdentityUserToken<string>>
{
    private readonly DatabaseOptions _settings;
    private new TenantInfo TenantInfo { get; set; }
    public IdentityDbContext(IMultiTenantContextAccessor<TenantInfo> multiTenantContextAccessor, DbContextOptions<IdentityDbContext> options, IOptions<DatabaseOptions> settings) : base(multiTenantContextAccessor, options)
    {
        _settings = settings.Value;
        TenantInfo = multiTenantContextAccessor.MultiTenantContext.TenantInfo!;
    }

    public DbSet<AuditTrail> AuditTrails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!string.IsNullOrWhiteSpace(TenantInfo?.ConnectionString))
        {
            optionsBuilder.ConfigureDatabase(_settings.Provider, TenantInfo.ConnectionString);
        }
    }
}
