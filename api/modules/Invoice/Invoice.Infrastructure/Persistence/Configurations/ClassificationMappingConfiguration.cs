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
internal class ClassificationMappingConfiguration : IEntityTypeConfiguration<ClassificationMapping>
{
    public void Configure(EntityTypeBuilder<ClassificationMapping> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.LhdnClassificationCode).IsRequired().HasMaxLength(50);
        builder.HasOne(e => e.Classification)
              .WithMany(u => u.ClassificationMappings)
              .HasForeignKey(e => e.ClassificationId);
        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
