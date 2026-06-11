using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductName)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(x => x.CreatedBy)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.ModifiedBy)
                   .HasMaxLength(100);
        }
    }
}
