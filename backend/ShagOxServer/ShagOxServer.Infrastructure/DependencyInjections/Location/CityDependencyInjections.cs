using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities;

namespace ShagOxServer.Infrastructure.DependencyInjections.Location;
public static class CityDependencyInjections
{
    public static IServiceCollection AddCityInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<ICityQueryRepository, CityQueryRepository>();
        services.AddScoped<ICityExistsRepository, CityExistsRepository>();

        return services;
    }
}