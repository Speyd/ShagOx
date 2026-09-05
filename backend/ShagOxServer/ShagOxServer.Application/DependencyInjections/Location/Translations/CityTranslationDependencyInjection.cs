using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Query;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Update;
using ShagOxServer.Application.Services.Location.Cities.Translations.Create;
using ShagOxServer.Application.Services.Location.Cities.Translations.Query;
using ShagOxServer.Application.Services.Location.Cities.Translations.Update;
using ShagOxServer.Application.Services.Location.Cities.Translations.Validator;

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

        //services.AddScoped<IRegionTranslationDeleteService,
        //    RegionTranslationDeleteService>();

        services.AddScoped<ICityTranslationUpdateService,
            CityTranslationUpdateService>();

        services.AddScoped<CityTranslationValidator>();

        return services;
    }
}