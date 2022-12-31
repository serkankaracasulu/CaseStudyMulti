using Infrastructure.Persistence;
using ProductAPI.Common.DAL;
using ProductAPI.Entities.Products;
using Redis.OM;

namespace ProductAPI.Applicaitons.Products
{
    public class ProductRepository : BaseRepository<Product, string>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext applicationDbContext, RedisConnectionProvider provider) : base(applicationDbContext, provider)
        {
        }
    }
}
