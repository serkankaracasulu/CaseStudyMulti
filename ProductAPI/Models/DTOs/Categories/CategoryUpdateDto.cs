using ProductAPI.Common.Mapping;
using ProductAPI.Entities.Categories;

namespace ProductAPI.Models.DTOs.Categories
{
    public class CategoryUpdateDto : IMapFrom<Category>
    {
        public string Name { get; set; }
    }
}
