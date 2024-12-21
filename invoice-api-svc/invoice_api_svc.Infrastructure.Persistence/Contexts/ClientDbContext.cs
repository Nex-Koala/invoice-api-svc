using invoice_api_svc.Domain.Entities.AP;
using invoice_api_svc.Domain.Entities.AR;
using invoice_api_svc.Domain.Entities.OE;
using invoice_api_svc.Domain.Entities.PO;
using Microsoft.EntityFrameworkCore;

namespace invoice_api_svc.Infrastructure.Persistence.Contexts
{
    public class ClientDbContext : DbContext
    {
        public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options) { }

        // DbSets for Order Entry (OE)
        public DbSet<SalesInvoiceHeader> OEINVH { get; set; }
        public DbSet<SalesInvoiceDetail> OEINVDD { get; set; }
        public DbSet<ShipmentHeader> OESHID { get; set; }
        public DbSet<ShipmentDetail> OESHIDS { get; set; }
        public DbSet<OrderCreditDebitHeader> OECRDH { get; set; }
        public DbSet<OrderCreditDebitDetail> OECRDD { get; set; }

        // DbSets for Accounts Receivable (AR)
        public DbSet<ReceivableInvoiceHeader> ARIBH { get; set; }
        public DbSet<ReceivableInvoiceDetail> ARIBD { get; set; }
        public DbSet<CustomerTransaction> ARCUS { get; set; }
        public DbSet<ReceivablesReference> ARGLREF { get; set; }

        // DbSets for Accounts Payable (AP)
        public DbSet<PayableInvoiceHeader> APIBH { get; set; }
        public DbSet<PayableInvoiceDetail> APIBD { get; set; }
        public DbSet<PayableReference> APGLREF { get; set; }
        public DbSet<VendorTransaction> APVEN { get; set; }

        // DbSets for Purchase Orders (PO)
        public DbSet<PurchaseInvoiceHeader> POINVH { get; set; }
        public DbSet<PurchaseInvoiceDetail> POINVD { get; set; }
        public DbSet<PurchaseOrderHeader> POPOR { get; set; }
        public DbSet<PurchaseReceipt> PORCPL { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // === Order Entry (OE) ===
            modelBuilder.Entity<SalesInvoiceHeader>()
                .ToTable("OEINVH")
                .HasKey(e => e.InvoiceId);

            modelBuilder.Entity<SalesInvoiceDetail>()
                .ToTable("OEINVDD")
                .HasKey(e => e.InvoiceDetailId);

            modelBuilder.Entity<ShipmentHeader>()
                .ToTable("OESHID")
                .HasKey(e => e.ShipmentId);

            modelBuilder.Entity<ShipmentDetail>()
                .ToTable("OESHIDS")
                .HasKey(e => e.ShipmentDetailId);

            modelBuilder.Entity<OrderCreditDebitHeader>()
                .ToTable("OECRDH")
                .HasKey(e => e.CreditDebitId);

            modelBuilder.Entity<OrderCreditDebitDetail>()
                .ToTable("OECRDD")
                .HasKey(e => e.CreditDebitDetailId);

            // === Accounts Receivable (AR) ===
            modelBuilder.Entity<ReceivableInvoiceHeader>()
                .ToTable("ARIBH")
                .HasKey(e => e.InvoiceId);

            modelBuilder.Entity<ReceivableInvoiceDetail>()
                .ToTable("ARIBD")
                .HasKey(e => e.InvoiceDetailId);

            modelBuilder.Entity<CustomerTransaction>()
                .ToTable("ARCUS")
                .HasKey(e => e.CustomerId);

            modelBuilder.Entity<ReceivablesReference>()
                .ToTable("ARGLREF")
                .HasKey(e => e.ReferenceId);

            // === Accounts Payable (AP) ===
            modelBuilder.Entity<PayableInvoiceHeader>()
                .ToTable("APIBH")
                .HasKey(e => e.InvoiceId);

            modelBuilder.Entity<PayableInvoiceDetail>()
                .ToTable("APIBD")
                .HasKey(e => e.InvoiceDetailId);

            modelBuilder.Entity<PayableReference>()
                .ToTable("APGLREF")
                .HasKey(e => e.ReferenceId);

            modelBuilder.Entity<VendorTransaction>()
                .ToTable("APVEN")
                .HasKey(e => e.VendorId);

            // === Purchase Orders (PO) ===
            modelBuilder.Entity<PurchaseInvoiceHeader>()
                .ToTable("POINVH")
                .HasKey(e => e.PurchaseInvoiceId);

            modelBuilder.Entity<PurchaseInvoiceDetail>()
                .ToTable("POINVD")
                .HasKey(e => e.PurchaseInvoiceDetailId);

            modelBuilder.Entity<PurchaseOrderHeader>()
                .ToTable("POPOR")
                .HasKey(e => e.PurchaseOrderId);

            modelBuilder.Entity<PurchaseReceipt>()
                .ToTable("PORCPL")
                .HasKey(e => e.ReceiptId);
        }
    }
}
