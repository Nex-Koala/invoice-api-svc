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

internal class UomMappingConfiguration : IEntityTypeConfiguration<UomMapping>
{
    public void Configure(EntityTypeBuilder<UomMapping> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.LhdnUomCode).IsRequired().HasMaxLength(50);
        builder.HasOne(e => e.Uom).WithMany(u => u.UomMappings).HasForeignKey(e => e.UomId);
        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
