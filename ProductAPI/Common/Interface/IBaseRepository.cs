using ProductAPI.Common.Model;

namespace ProductAPI.Common.Interface
{
    public interface IBaseRepository<T,PK> : IDisposable
    {
        T Delete(T entity);
        ValueTask<T?> Get(PK key);
        IQueryable<T> GetQueryable();
        ValueTask<T> Insert(T entity);
        Task<PaginatedList<Dto>> List<Dto>(AutoMapper.IConfigurationProvider configuration, int pageNumber, int pageSize);
        Task<int> Save();
        T Update(T entity);
    }
}
