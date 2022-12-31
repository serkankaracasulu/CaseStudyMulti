using ProductAPI.Common.Mapping;
using ProductAPI.Entities.Categories;

namespace ProductAPI.Models.DTOs.Categories
{
    public class CategoryDto : IMapFrom<Category>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
