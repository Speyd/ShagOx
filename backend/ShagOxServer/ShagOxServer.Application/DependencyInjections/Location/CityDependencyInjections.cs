using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Location.Translations;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Delete;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Query;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Update;
using ShagOxServer.Application.Services.Location.Cities.Delete;
using ShagOxServer.Application.Services.Location.Cities.Query;
using ShagOxServer.Application.Services.Location.Cities.Update;
using ShagOxServer.Application.Services.Location.Cities.Update.Validator;
using ShagOxServer.Application.Services.Location.Cities.Validator;

namespace ShagOxServer.Application.DependencyInjections.Location;
public static class CityDependencyInjections
{
    public static IServiceCollection AddCityApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ICityQueryService, CityQueryService>();
        services.AddScoped<ICityUpdateService, CityUpdateService>();
        services.AddScoped<ICityDeleteService, CityDeleteService>();

        services.AddScoped<CityValidator>();
        services.AddScoped<CityUpdateValidator>();

        services.AddCityTranslationApplication();

        return services;
    }
}