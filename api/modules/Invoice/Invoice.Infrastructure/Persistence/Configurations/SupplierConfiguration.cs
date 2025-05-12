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

internal class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(100);
        builder.Property(x => x.Tin).HasMaxLength(20);
        builder.Property(x => x.IdType).HasMaxLength(20);
        builder.Property(x => x.Brn).HasMaxLength(20);
        builder.Property(x => x.SstRegistrationNumber).HasMaxLength(50);
        builder.Property(x => x.TaxTourismRegistrationNumber).HasMaxLength(20);
        builder.Property(x => x.MsicCode).HasMaxLength(20);
        builder.Property(x => x.BusinessActivityDescription).HasMaxLength(300);
        builder.Property(x => x.Email).HasMaxLength(50);
        builder.Property(x => x.ContactNumber).HasMaxLength(50);
        builder.Property(x => x.Address).HasMaxLength(100);
        builder.Property(x => x.City).HasMaxLength(50);
        builder.Property(x => x.PostalCode).HasMaxLength(10);
        builder.Property(x => x.CountryCode).HasMaxLength(10);
    }
}
