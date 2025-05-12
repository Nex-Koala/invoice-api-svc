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

internal class InvoiceLineConfiguration : IEntityTypeConfiguration<InvoiceLine>
{
    public void Configure(EntityTypeBuilder<InvoiceLine> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.LineNumber).HasMaxLength(50);
        builder.Property(x => x.Quantity).HasColumnType("decimal(18,2)");
        builder.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)");
        builder.Property(x => x.LineAmount).HasColumnType("decimal(18,2)");
        builder.Property(x => x.TaxAmount).HasColumnType("decimal(18,2)");
        builder.Property(x => x.Description).HasMaxLength(100);
        builder.Property(x => x.UnitCode).HasMaxLength(10);
        builder.Property(x => x.CurrencyCode).HasMaxLength(3);

        builder
            .HasOne(x => x.InvoiceDocument)
            .WithMany(d => d.InvoiceLines)
            .HasForeignKey(x => x.InvoiceDocumentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
