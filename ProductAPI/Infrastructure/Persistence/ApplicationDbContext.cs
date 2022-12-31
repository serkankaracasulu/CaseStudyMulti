using Microsoft.EntityFrameworkCore;
using ProductAPI.Entities.Products;
using ProductAPI.Entities.Categories;
using System.Reflection;
using ProductAPI.Infrastructure.Persistence.Interceptors;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly RedisSaveChangesInterceptor _redisSaveChanges;

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, RedisSaveChangesInterceptor redisSaveChanges) : base(options)
        {
            _redisSaveChanges = redisSaveChanges;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_redisSaveChanges);
        }
    }
}
