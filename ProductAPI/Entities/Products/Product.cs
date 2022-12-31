using ProductAPI.Common.Mapping;
using ProductAPI.Entities.Categories;
using ProductAPI.Entities.ProductImages;
using ProductAPI.Models.DTOs.Products;
using Redis.OM.Modeling;

namespace ProductAPI.Entities.Products
{
    [Document(StorageType = StorageType.Json, Prefixes = new[] { nameof(Product) })]
    public class Product : IMapFrom<ProductCreateDto>, IMapFrom<ProductUpdateDto>
    {
        [RedisIdField]
        [Indexed]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Category> Catogories { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
