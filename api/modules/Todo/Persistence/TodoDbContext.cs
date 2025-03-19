using Finbuckle.MultiTenant.Abstractions;
using NexKoala.Framework.Core.Persistence;
using NexKoala.Framework.Infrastructure.Persistence;
using NexKoala.Framework.Infrastructure.Tenant;
using NexKoala.WebApi.Todo.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace NexKoala.WebApi.Todo.Persistence;
public sealed class TodoDbContext : FshDbContext
{
    public TodoDbContext(IMultiTenantContextAccessor<TenantInfo> multiTenantContextAccessor, DbContextOptions<TodoDbContext> options, IPublisher publisher, IOptions<DatabaseOptions> settings)
        : base(multiTenantContextAccessor, options, publisher, settings)
    {
    }

    public DbSet<TodoItem> Todos { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoDbContext).Assembly);
        modelBuilder.HasDefaultSchema(SchemaNames.Todo);
    }
}
