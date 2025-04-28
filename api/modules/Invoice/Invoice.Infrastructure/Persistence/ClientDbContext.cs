using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexKoala.WebApi.Invoice.Domain.Entities.AP;
using NexKoala.WebApi.Invoice.Domain.Entities.AR;
using NexKoala.WebApi.Invoice.Domain.Entities.OE;
using NexKoala.WebApi.Invoice.Domain.Entities.PO;

namespace NexKoala.WebApi.Invoice.Infrastructure.Persistence;
public class ClientDbContext : DbContext
{
    public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options) { }

    // Order Entry Entities
    public DbSet<OrderEntryHeader> OrderEntryHeaders { get; set; }
    public DbSet<OrderEntryDetail> OrderEntryDetails { get; set; }

    // Credit/Debit Notes (Order Entry)
    public DbSet<OrderCreditDebitHeader> OrderCreditDebitHeaders { get; set; }
    public DbSet<OrderCreditDebitDetail> OrderCreditDebitDetails { get; set; }

    // Purchase Invoice (Self Billing)
    public DbSet<PurchaseInvoiceHeader> PurchaseInvoiceHeaders { get; set; }
    public DbSet<PurchaseInvoiceDetail> PurchaseInvoiceDetails { get; set; }

    // Purchase Credit/Debit Notes (Self Billing)
    public DbSet<PurchaseCreditDebitNoteHeader> PurchaseCreditNoteHeaders { get; set; }
    public DbSet<PurchaseCreditDebitNoteDetail> PurchaseCreditNoteDetails { get; set; }

    // Account Receivable Customer Entities
    public DbSet<AccountReceivableCustomer> AccountReceivableCustomers { get; set; }

    // Account Payable Vendor Entities
    public DbSet<AccountPayableVendor> AccountPayableVendors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("dbo");
        // Configure Sales Invoice Tables
        modelBuilder.Entity<OrderEntryHeader>()
            .ToTable("OEINVH")
            .Ignore(o => o.CustomerTIN)
            .Ignore(o => o.CustomerBRN)
            .HasKey(o => o.INVUNIQ);

        modelBuilder.Entity<OrderEntryDetail>()
            .ToTable("OEINVD")
            .HasKey(d => new { d.INVUNIQ });

        modelBuilder.Entity<OrderEntryHeader>()
            .HasMany(o => o.OrderEntryDetails)
            .WithOne(d => d.OrderEntryHeader)
            .HasForeignKey(d => d.INVUNIQ);

        // Configure Credit/Debit Note Tables (Order Entry)
        modelBuilder.Entity<OrderCreditDebitHeader>()
            .ToTable("OECRDH")
            .Ignore(o => o.CustomerTIN)
            .Ignore(o => o.CustomerBRN)
            .HasKey(o => o.CRDUNIQ);

        modelBuilder.Entity<OrderCreditDebitDetail>()
            .ToTable("OECRDD")
            .HasKey(d => new { d.CRDUNIQ, d.LINENUM });

        modelBuilder.Entity<OrderCreditDebitHeader>()
            .HasMany(o => o.OrderCreditDebitDetails)
            .WithOne(d => d.OrderCreditDebitHeader)
            .HasForeignKey(d => d.CRDUNIQ);

        // Configure Purchase Invoice Tables (Self Billing)
        modelBuilder.Entity<PurchaseInvoiceHeader>()
            .ToTable("POINVH1")
            .Ignore(o => o.SupplierTIN)
            .Ignore(o => o.SupplierBRN)
            .HasKey(p => p.INVHSEQ);

        modelBuilder.Entity<PurchaseInvoiceDetail>()
            .ToTable("POINVL")
            .HasKey(p => new { p.INVHSEQ });

        modelBuilder.Entity<PurchaseInvoiceHeader>()
            .HasMany(p => p.PurchaseInvoiceDetails)
            .WithOne(p => p.PurchaseInvoiceHeader)
            .HasForeignKey(p => p.INVHSEQ);


        // Configure Purchase Credit/Debit Note Tables (Self Billing)
        modelBuilder.Entity<PurchaseCreditDebitNoteHeader>()
            .ToTable("POCRNH1")
            .Ignore(o => o.SupplierTIN)
            .Ignore(o => o.SupplierBRN)
            .HasKey(p => p.CRNHSEQ);

        modelBuilder.Entity<PurchaseCreditDebitNoteDetail>()
            .ToTable("POCRNL")
            .HasKey(p => new { p.CRNHSEQ, p.CRNLREV });

        modelBuilder.Entity<PurchaseCreditDebitNoteHeader>()
            .HasMany(p => p.PurchaseCreditDebitNoteDetails)
            .WithOne(d => d.PurchaseCreditDebitNoteHeader)
            .HasForeignKey(d => d.CRNHSEQ);

        // Account Receivable Customer
        modelBuilder.Entity<AccountReceivableCustomer>()
            .ToTable("ARCUS")
            .HasKey(c => c.IDCUST);

        modelBuilder.Entity<AccountPayableVendor>()
            .ToTable("APVEN")
            .HasKey("VENDORID");

        ConfigureFieldMappings(modelBuilder);
    }

    private void ConfigureFieldMappings(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<AccountPayableInvoiceHeader>().Property(p => p.SUPPLIERID).HasColumnName("SUPPLIER");
        //modelBuilder.Entity<AccountPayableInvoiceDetail>().Property(p => p.ITEMDESC).HasColumnName("ITEMDESC");
    }
}
