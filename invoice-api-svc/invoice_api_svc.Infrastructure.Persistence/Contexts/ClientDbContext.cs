using Microsoft.EntityFrameworkCore;
using invoice_api_svc.Domain.Entities.AP;
using invoice_api_svc.Domain.Entities.AR;
using invoice_api_svc.Domain.Entities.OE;
using invoice_api_svc.Domain.Entities.PO;

namespace invoice_api_svc.Infrastructure.Persistence.Contexts
{
    /// <summary>
    /// DbContext for managing entities and database interactions.
    /// </summary>
    public class ClientDbContext : DbContext
    {
        public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options) { }

        // DbSet for Purchase Invoices
        public DbSet<PurchaseInvoiceHeader> PurchaseInvoiceHeaders { get; set; }
        public DbSet<PurchaseInvoiceDetail> PurchaseInvoiceDetails { get; set; }

        // DbSet for Receivables
        public DbSet<ReceivableInvoiceHeader> ReceivableInvoiceHeaders { get; set; }
        public DbSet<ReceivableInvoiceDetail> ReceivableInvoiceDetails { get; set; }

        // DbSet for Order Entries
        public DbSet<OrderEntryHeader> OrderEntryHeaders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Purchase Invoice Header
            modelBuilder.Entity<PurchaseInvoiceHeader>()
                .ToTable("POINVH1")
                .HasKey(p => p.INVNUMBER);

            modelBuilder.Entity<PurchaseInvoiceDetail>()
                .ToTable("POINVL")
                .HasKey(p => new { p.INVNUMBER, p.LineNumber });

            // Receivable Invoice Header
            modelBuilder.Entity<ReceivableInvoiceHeader>()
                .ToTable("ARIBH")
                .HasKey(r => r.INVNUMBER);

            modelBuilder.Entity<ReceivableInvoiceDetail>()
                .ToTable("ARIBD")
                .HasKey(r => new { r.INVNUMBER, r.LINENUMBER });

            // Order Entry Header
            modelBuilder.Entity<OrderEntryHeader>()
                .ToTable("OEINVH")
                .HasKey(o => o.ORDERID);

            ConfigureFieldMappings(modelBuilder);
        }

        private void ConfigureFieldMappings(ModelBuilder modelBuilder)
        {
            // Purchase Invoice Header Field Mappings
            modelBuilder.Entity<PurchaseInvoiceHeader>().Property(p => p.VDNAME).HasColumnName("VDNAME");
            modelBuilder.Entity<PurchaseInvoiceHeader>().Property(p => p.VDEMAIL).HasColumnName("VDEMAIL");

            // Purchase Invoice Detail Field Mappings
            modelBuilder.Entity<PurchaseInvoiceDetail>().Property(p => p.ITEMDESC).HasColumnName("ITEMDESC");

            // Receivable Invoice Header Field Mappings
            modelBuilder.Entity<ReceivableInvoiceHeader>().Property(r => r.CUSTOMERID).HasColumnName("CUSTOMERID");

            // Order Entry Header Field Mappings
            modelBuilder.Entity<OrderEntryHeader>().Property(o => o.CUSTOMERID).HasColumnName("CUSTOMERID");
        }
    }
}
