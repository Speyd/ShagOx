using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories;

namespace ShagOxServer.Infrastructure.DependencyInjections;
public static class BaseDependencyInjection
{
    public static IServiceCollection AddBaseRepositories(
       this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

        return services;
    }
}