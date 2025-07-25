using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexKoala.WebApi.Invoice.Domain.Entities;

namespace NexKoala.WebApi.Invoice.Infrastructure.Persistence.Configurations;
internal class LicenseKeyConfiguration : IEntityTypeConfiguration<LicenseKey>
{
    public void Configure(EntityTypeBuilder<LicenseKey> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Key)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => x.Key)
            .IsUnique();

        builder.Property(x => x.PartnerId)
            .IsRequired();

        builder.Property(x => x.MaxSubmissions).IsRequired();
        builder.Property(x => x.SubmissionCount).IsRequired();
        builder.Property(x => x.ExpiryDate).IsRequired();
        builder.Property(x => x.IsRevoked).IsRequired();

        builder.HasOne(x => x.Partner)
            .WithOne(p => p.LicenseKey)
            .HasForeignKey<LicenseKey>(x => x.PartnerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
