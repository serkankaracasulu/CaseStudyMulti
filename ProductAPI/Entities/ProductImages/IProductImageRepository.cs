using ProductAPI.Common.Interface;
using ProductAPI.Common.Model;

namespace ProductAPI.Entities.ProductImages
{
    public interface IProductImageRepository : IBaseRepository<ProductImage, string>
    {
        Task<PaginatedList<ProductImage>> List<ProductImage>(AutoMapper.IConfigurationProvider configuration, string productId, int pageNumber, int pageSize);
    }
}
