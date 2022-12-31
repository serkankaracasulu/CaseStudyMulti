using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ProductAPI.Entities.Categories;
using ProductAPI.Entities.Common;
using ProductAPI.Entities.Products;
using Redis.OM;
using Redis.OM.Searching;

namespace ProductAPI.Infrastructure.Persistence.Interceptors
{
    public class RedisSaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly RedisConnectionProvider _provider;
        public RedisSaveChangesInterceptor(RedisConnectionProvider provider)
        {
            _provider = provider;
        }
        public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            await UpdateRedis(eventData.Context);
            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }
        public async Task UpdateRedis(DbContext? context)
        {
            if (context == null) return;
            var k = context.ChangeTracker.Entries().ToList();
            await RedisSaveChanges<Product, string>.SaveAsync(_provider, context.ChangeTracker.Entries<Product>());
            await RedisSaveChanges<Category, Guid>.SaveAsync(_provider, context.ChangeTracker.Entries<Category>());
        }
    }
    public class RedisSaveChanges<T, PK> where T : class
    {
        private readonly RedisCollection<T> _collection;
        private readonly IEnumerable<T> _addedEntity;
        private readonly IEnumerable<T> _deletedEntity;
        private readonly IEnumerable<T> _updatedEntity;

        public RedisSaveChanges(RedisConnectionProvider provider, IEnumerable<EntityEntry<T>> entries)
        {
            _collection = (RedisCollection<T>)provider.RedisCollection<T>();
            _addedEntity = entries.Where(d => d.State == EntityState.Added || d.State == EntityState.Unchanged).Select(d => d.Entity);
            _deletedEntity = entries.Where(d => d.State == EntityState.Deleted).Select(d => d.Entity);
            _updatedEntity = entries.Where(d => d.State == EntityState.Modified).Select(d => d.Entity);
        }

        public static async Task SaveAsync(RedisConnectionProvider provider, IEnumerable<EntityEntry<T>> entries)
        {
            var redisSaveChanges = new RedisSaveChanges<T, PK>(provider, entries);
            await redisSaveChanges.SaveAllAsync();
        }

        public async Task InsertAllAsync()
        {
            foreach (var item in _addedEntity)
            {
                await _collection.InsertAsync(item);
            }
        }
        public async Task DeleteAllAsync()
        {
            foreach (var item in _deletedEntity)
            {
                await _collection.DeleteAsync(item);
            }
        }
        public async Task UpdateAllAsync()
        {
            foreach (var item in _updatedEntity)
            {
                await _collection.UpdateAsync(item);
            }
        }
        public async Task SaveAllAsync()
        {
            await UpdateAllAsync();
            await InsertAllAsync();
            await DeleteAllAsync();
        }
    }
}
