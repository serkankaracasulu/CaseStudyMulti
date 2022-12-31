using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductAPI.Entities.Products;
using ProductAPI.Entities.ProductImages;
using ProductAPI.Entities.Products.Categories;

namespace ProductAPI.Infrastructure.Persistence.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.HasOne<Product>().WithMany(d => d.ProductImages).HasForeignKey(x => x.ProductId);
        }
    }
}
