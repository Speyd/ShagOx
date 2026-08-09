using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions;

namespace ShagOxServer.Infrastructure.DependencyInjections;
public static class LocationDependencyInjection
{
    public static IServiceCollection AddLocationInfrastructure(this IServiceCollection services)
    {

        // Region
        services.AddScoped<IRegionQueryRepository, RegionQueryRepository>();
        services.AddScoped<IRegionExistsRepository, RegionExistsRepository>();

        // City
        services.AddScoped<ICityQueryRepository, CityQueryRepository>();
        services.AddScoped<ICityExistsRepository, CityExistsRepository>();

        return services;
    }
}