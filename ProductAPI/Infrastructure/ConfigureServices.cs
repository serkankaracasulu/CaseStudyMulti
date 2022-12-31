using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Infrastructure.Persistence.Interceptors;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("CaseStudyDb"));
        services.AddScoped<RedisSaveChangesInterceptor>();
        return services;
    }
}
