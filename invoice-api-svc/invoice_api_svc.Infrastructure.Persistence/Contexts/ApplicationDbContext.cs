using invoice_api_svc.Application.Interfaces;
using invoice_api_svc.Domain.Common;
using invoice_api_svc.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;

namespace invoice_api_svc.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IDateTimeService dateTime,
            IAuthenticatedUserService authenticatedUser,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<InvoiceDocument> InvoiceDocuments { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Configure InvoiceDocument to Supplier relationship
            builder.Entity<InvoiceDocument>()
                .HasOne(i => i.Supplier)
                .WithMany()
                .HasForeignKey(i => i.SupplierId); // Use FK property

            // Configure InvoiceDocument to Customer relationship
            builder.Entity<InvoiceDocument>()
                .HasOne(i => i.Customer)
                .WithMany()
                .HasForeignKey(i => i.CustomerId); // Use FK property

            // Configure InvoiceLine to InvoiceDocument relationship
            builder.Entity<InvoiceLine>()
                .HasOne(il => il.InvoiceDocument)
                .WithMany(i => i.InvoiceLines)
                .HasForeignKey(il => il.InvoiceDocumentId); // Use FK property

            builder.Entity<Uom>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            builder.Entity<UomMapping>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.LhdnUomCode).IsRequired().HasMaxLength(50);
                entity.HasOne(e => e.Uom)
                      .WithMany(u => u.UomMappings)
                      .HasForeignKey(e => e.UomId);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // Apply global decimal configuration
            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }

            base.OnModelCreating(builder);
        }
    }
}
