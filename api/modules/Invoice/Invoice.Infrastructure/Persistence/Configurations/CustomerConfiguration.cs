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
internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(100);
        builder.Property(x => x.Tin).HasMaxLength(20);
        builder.Property(x => x.Brn).HasMaxLength(20);
        builder.Property(x => x.Address).HasMaxLength(100);
        builder.Property(x => x.City).HasMaxLength(50);
        builder.Property(x => x.PostalCode).HasMaxLength(10);
        builder.Property(x => x.CountryCode).HasMaxLength(10);
    }
}
