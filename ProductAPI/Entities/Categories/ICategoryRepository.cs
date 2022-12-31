using ProductAPI.Common.Interface;
using ProductAPI.Common.Model;
using ProductAPI.Models.DTOs.Categories;

namespace ProductAPI.Entities.Categories
{
    public interface ICategoryRepository : IBaseRepository<Category, string>
    {
        Task<PaginatedList<CategoryDto>> List<CategoryDto>(AutoMapper.IConfigurationProvider configuration, string productId, int pageNumber, int pageSize);
    }
}
