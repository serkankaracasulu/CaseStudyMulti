using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductAPI.Entities.Products;
using ProductAPI.Entities.Categories;
using ProductAPI.Entities.Products.Categories;

namespace ProductAPI.Infrastructure.Persistence.Configurations
{
    public class CategoriesConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(d => d.Name).HasMaxLength(CategoryConsts.NameMaxLength);

            builder.HasOne<Product>().WithMany(d => d.Catogories).HasForeignKey(x => x.ProductId);
        }
    }
}
