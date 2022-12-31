using ProductAPI.Common.Mapping;
using ProductAPI.Entities.Products;

namespace ProductAPI.Models.DTOs.Products
{
    public class ProductCreateDto : IMapFrom<Product>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
