using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Caches;

namespace ShagOxServer.Application.DependencyInjections.Caches;
public static class CacheServiceDependencyInjection
{
    public static IServiceCollection AddCacheApplication(
        this IServiceCollection services)
    {
        services.AddSingleton<ICacheService,
            CacheService>();

        return services;
    }
}