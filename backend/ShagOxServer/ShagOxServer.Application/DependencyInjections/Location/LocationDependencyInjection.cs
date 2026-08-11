using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Application.DependencyInjections.Location;
public static class LocationDependencyInjection
{
    public static IServiceCollection AddLocationApplication(
        this IServiceCollection services)
    {
        services.AddRegionApplication();

        services.AddCityApplication();

        return services;
    }
}