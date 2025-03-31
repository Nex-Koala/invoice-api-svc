using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Infrastructure.Persistence.Configurations;

internal class InvoiceDocumentConfiguration : IEntityTypeConfiguration<InvoiceDocument>
{
    public void Configure(EntityTypeBuilder<InvoiceDocument> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.InvoiceNumber).IsRequired().HasMaxLength(50);
        builder.Property(x => x.IssueDate).IsRequired();
        builder.Property(x => x.DocumentCurrencyCode).IsRequired().HasMaxLength(3);
        builder.Property(x => x.TaxCurrencyCode).HasMaxLength(3);
        builder.Property(x => x.TotalAmount).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.TaxAmount).IsRequired().HasColumnType("decimal(18,2)");

        builder
            .HasOne(x => x.Supplier)
            .WithMany()
            .HasForeignKey(x => x.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(x => x.InvoiceLines)
            .WithOne(x => x.InvoiceDocument)
            .HasForeignKey(x => x.InvoiceDocumentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
