using ProductAPI.Common.Mapping;
using ProductAPI.Entities.Categories;

namespace ProductAPI.Models.DTOs.Categories
{
    public class CategoryCreateDto : IMapFrom<Category>
    {
        public string Name { get; set; }
        public string ProductId { get; set; }
    }
}
