using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Translations;

namespace ShagOxServer.Infrastructure.DependencyInjections.Location.Translations;
public static class RegionTranslationDependencyInjections
{
    public static IServiceCollection AddRegionTranslationInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IRegionTranslationQueryRepository, 
            RegionTranslationQueryRepository>();

        services.AddScoped<IRegionTranslationExistsRepository, 
            RegionTranslationExistsRepository>();

        return services;
    }
}