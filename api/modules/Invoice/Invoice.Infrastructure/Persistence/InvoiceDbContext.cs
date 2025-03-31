using Finbuckle.MultiTenant.Abstractions;
using NexKoala.Framework.Infrastructure.Persistence;
using NexKoala.Framework.Infrastructure.Tenant;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NexKoala.WebApi.Invoice.Domain.Entities;
using NexKoala.Framework.Core.Persistence;

namespace NexKoala.WebApi.Invoice.Infrastructure.Persistence;

public sealed class InvoiceDbContext : FshDbContext
{
    public InvoiceDbContext(IMultiTenantContextAccessor<TenantInfo> multiTenantContextAccessor, DbContextOptions<InvoiceDbContext> options, IPublisher publisher, IOptions<DatabaseOptions> settings)
        : base(multiTenantContextAccessor, options, publisher, settings)
    {
    }

    public DbSet<InvoiceDocument> InvoiceDocuments { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<InvoiceLine> InvoiceLines { get; set; }
    public DbSet<Partner> Partners { get; set; }
    public DbSet<Classification> Classifications { get; set; }
    public DbSet<ClassificationMapping> ClassificationMappings { get; set; }
    public DbSet<Uom> Uoms { get; set; }
    public DbSet<UomMapping> UomMappings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InvoiceDbContext).Assembly);
        modelBuilder.HasDefaultSchema(SchemaNames.Invoice);
    }
}
