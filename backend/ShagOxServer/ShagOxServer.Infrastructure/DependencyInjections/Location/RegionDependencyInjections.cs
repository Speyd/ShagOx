using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Infrastructure.DependencyInjections.Location.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions;

namespace ShagOxServer.Infrastructure.DependencyInjections.Location;
public static class RegionDependencyInjections
{
    public static IServiceCollection AddRegionInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IRegionQueryRepository, RegionQueryRepository>();
        services.AddScoped<IRegionExistsRepository, RegionExistsRepository>();

        services.AddRegionTranslationInfrastructure();

        return services;
    }
}