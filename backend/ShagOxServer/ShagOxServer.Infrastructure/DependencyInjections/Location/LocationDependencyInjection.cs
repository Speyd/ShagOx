using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Infrastructure.DependencyInjections.Location;
public static class LocationDependencyInjection
{
    public static IServiceCollection AddLocationInfrastructure(this IServiceCollection services)
    {
        services.AddRegionInfrastructure();

        services.AddCityInfrastructure();

        return services;
    }
}