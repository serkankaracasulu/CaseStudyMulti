using ProductAPI.Common.Interface;

namespace ProductAPI.Entities.Products
{
    public interface IProductRepository : IBaseRepository<Product, string>
    {
    }
}
