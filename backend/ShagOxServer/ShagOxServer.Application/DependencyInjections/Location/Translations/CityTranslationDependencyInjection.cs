using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Query;
using ShagOxServer.Application.Services.Location.Cities.Translations.Create;
using ShagOxServer.Application.Services.Location.Cities.Translations.Query;

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

        //services.AddScoped<IRegionTranslationUpdateService,
        //    RegionTranslationUpdateService>();

        //services.AddScoped<RegionTranslationValidator>();

        return services;
    }
}