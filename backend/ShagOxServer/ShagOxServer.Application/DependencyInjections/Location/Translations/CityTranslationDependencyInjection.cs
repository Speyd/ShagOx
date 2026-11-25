using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Query;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Update;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Translation;
using ShagOxServer.Application.Services.Caches.Invalidations.Location.Translation;
using ShagOxServer.Application.Services.Location.Cities.Translations.Create;
using ShagOxServer.Application.Services.Location.Cities.Translations.Delete;
using ShagOxServer.Application.Services.Location.Cities.Translations.Query;
using ShagOxServer.Application.Services.Location.Cities.Translations.Update;
using ShagOxServer.Application.Services.Location.Cities.Translations.Validator;
using ShagOxServer.Domain.Cache.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Application.DependencyInjections.Location.Translations;
public static class CityTranslationDependencyInjection
{
    public static IServiceCollection AddCityTranslationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ICityTranslationQueryService,
            CityTranslationQueryService>();

        services.AddScoped<ICityTranslationCreateService,
            CityTranslationCreateService>();

        services.AddScoped<ICityTranslationDeleteService,
            CityTranslationDeleteService>();

        services.AddScoped<ICityTranslationUpdateService,
            CityTranslationUpdateService>();

        services.AddScoped<
            ICacheInvalidationService<CityTranslation, TranslationCacheContext>,
            CityTranslationInvalidationService>();

        services.AddScoped<CityTranslationValidator>();

        return services;
    }
}
