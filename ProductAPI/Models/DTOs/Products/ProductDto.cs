using ProductAPI.Common.Mapping;
using ProductAPI.Entities.Products;

namespace ProductAPI.Models.DTOs.Products
{
    public class ProductDto : IMapFrom<Product>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
