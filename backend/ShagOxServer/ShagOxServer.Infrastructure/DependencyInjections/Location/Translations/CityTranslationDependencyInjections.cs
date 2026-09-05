using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Translations;

namespace ShagOxServer.Infrastructure.DependencyInjections.Location.Translations;
public static class CityTranslationDependencyInjections
{
    public static IServiceCollection AddCityTranslationInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<ICityTranslationQueryRepository,
            CityTranslationQueryRepository>();

        services.AddScoped<ICityTranslationExistsRepository,
            CityTranslationExistsRepository>();

        return services;
    }
}