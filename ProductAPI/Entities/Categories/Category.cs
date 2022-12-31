using ProductAPI.Common.Mapping;
using ProductAPI.Models.DTOs.Categories;
using Redis.OM.Modeling;

namespace ProductAPI.Entities.Categories
{
    [Document(StorageType = StorageType.Json, Prefixes = new[] { nameof(Category) })]
    public class Category : IMapFrom<CategoryCreateDto>, IMapFrom<CategoryUpdateDto>
    {
        [RedisIdField]
        [Indexed]
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string Name { get; set; }
    }
}
