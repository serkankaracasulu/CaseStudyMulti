using FluentValidation;
using FluentValidation.AspNetCore;
using ProductAPI.Applicaitons.Categories;
using ProductAPI.Applicaitons.Images;
using ProductAPI.Applicaitons.Products;
using ProductAPI.Entities.Categories;
using ProductAPI.Entities.ProductImages;
using ProductAPI.Entities.Products;
using ProductAPI.HostedService;
using Redis.OM;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicaitonConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductImageRepository, ImageRepository>();
            services.AddHostedService<IndexCreationService>();
            services.AddSingleton(new RedisConnectionProvider(configuration["REDIS_CONNECTION_STRING"]));
            services.AddCors(options =>
            {
                options.AddPolicy(name: "localhost",
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:3000", "https://localhost:3000").AllowAnyHeader().AllowAnyMethod();

                                  });
            });
            return services;
        }
    }
}
