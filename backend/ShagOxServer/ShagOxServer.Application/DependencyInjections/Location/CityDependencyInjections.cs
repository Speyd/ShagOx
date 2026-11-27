using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Location.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Create;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Delete;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Query;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Update;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries;
using ShagOxServer.Application.Services.Caches.Invalidations.Location;
using ShagOxServer.Application.Services.Caches.Invalidations.Location.Translation;
using ShagOxServer.Application.Services.Location.Cities.Create;
using ShagOxServer.Application.Services.Location.Cities.Delete;
using ShagOxServer.Application.Services.Location.Cities.Query;
using ShagOxServer.Application.Services.Location.Cities.Update;
using ShagOxServer.Application.Services.Location.Cities.Update.Validator;
using ShagOxServer.Application.Services.Location.Cities.Validator;
using ShagOxServer.Domain.Cache.Dictionaries;
using ShagOxServer.Domain.Cache.Location;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.DependencyInjections.Location;
public static class CityDependencyInjections
{
    public static IServiceCollection AddCityApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ICityQueryService, 
            CityQueryService>();

        services.AddScoped<ICityCreateService,
            CityCreateService>();

        services.AddScoped<ICityUpdateService,
            CityUpdateService>();

        services.AddScoped<ICityDeleteService,
            CityDeleteService>();

        services.AddScoped<CityValidator>();
        services.AddScoped<CityUpdateValidator>();

        services.AddScoped<
            ICacheInvalidationService<City, CityCacheContext>,
            CityInvalidationService>();

        services.AddScoped<CityInvalidationService>();

        services.AddCityTranslationApplication();

        return services;
    }
}
