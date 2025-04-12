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

internal class PartnerConfiguration : IEntityTypeConfiguration<Partner>
{
    public void Configure(EntityTypeBuilder<Partner> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

        builder.Property(x => x.CompanyName).IsRequired().HasMaxLength(100);

        builder.Property(x => x.Tin).HasMaxLength(20);

        builder.Property(x => x.SchemeId).HasMaxLength(20);

        builder.Property(x => x.RegistrationNumber).HasMaxLength(20);

        builder.Property(x => x.SstRegistrationNumber).HasMaxLength(20);

        builder.Property(x => x.TourismTaxRegistrationNumber).HasMaxLength(30);

        builder.Property(x => x.Email).IsRequired().HasMaxLength(150);

        builder.Property(x => x.Phone).IsRequired().HasMaxLength(20);

        builder.Property(x => x.MsicCode).HasMaxLength(20);

        builder.Property(x => x.BusinessActivityDescription).HasMaxLength(200);

        builder.Property(x => x.Address1).IsRequired().HasMaxLength(255);

        builder.Property(x => x.Address2).HasMaxLength(255);

        builder.Property(x => x.Address3).HasMaxLength(255);

        builder.Property(x => x.PostalCode).HasMaxLength(10);

        builder.Property(x => x.City).HasMaxLength(50);

        builder.Property(x => x.State).HasMaxLength(50);

        builder.Property(x => x.CountryCode).HasMaxLength(10);

        builder.Property(x => x.LicenseKey).IsRequired().HasMaxLength(50);

        builder.Property(x => x.Status).IsRequired();

        builder.Property(x => x.SubmissionCount).IsRequired();

        builder.Property(x => x.MaxSubmissions).IsRequired();
    }
}
