using AutoMapper.QueryableExtensions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Common.Interface;
using ProductAPI.Common.Model;
using Redis.OM;
using Redis.OM.Searching;

namespace ProductAPI.Common.DAL
{
    public abstract class BaseRepository<T, PK> : IBaseRepository<T, PK> where T : class
    {
        protected readonly DbSet<T> _db;
        public readonly ApplicationDbContext applicationDbContext;
        private readonly RedisCollection<T> _collection;
        private readonly RedisConnectionProvider _provider;

        public BaseRepository(ApplicationDbContext applicationDbContext, RedisConnectionProvider provider)
        {
            _db = applicationDbContext.Set<T>();
            this.applicationDbContext = applicationDbContext;
            _provider = provider;
            _collection = (RedisCollection<T>)provider.RedisCollection<T>();
        }
        public async ValueTask<T> Insert(T entity)
        {
            var result = await _db.AddAsync(entity);
            return result.Entity;
        }
        public T Update(T entity)
        {
            var result = _db.Update(entity);
            return result.Entity;
        }
        public Task<PaginatedList<Dto>> List<Dto>(AutoMapper.IConfigurationProvider configuration, int pageNumber, int pageSize)
        {
            return PaginatedList<Dto>.CreateAsync(_db.ProjectTo<Dto>(configuration), pageNumber, pageSize);
        }
        public T Delete(T entity)
        {
            var result = _db.Remove(entity);
            return result.Entity;
        }
        public IQueryable<T> GetQueryable()
        {
            return _db.AsQueryable();
        }
        public async ValueTask<T?> Get(PK key)
        {
            var entity = await _collection.FindByIdAsync(key.ToString());
            if (entity != null) return entity;
            return await _db.FindAsync(key);
        }
        public async Task<int> Save() => await applicationDbContext.SaveChangesAsync();
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    applicationDbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
