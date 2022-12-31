using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductAPI.Entities;
using ProductAPI.Entities.Products;

namespace ProductAPI.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(d => d.Description).HasMaxLength(ProductConsts.DescriptionMaxLength);
        }
    }
}
