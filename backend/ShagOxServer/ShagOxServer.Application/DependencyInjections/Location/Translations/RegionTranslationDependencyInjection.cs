using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Query;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Update;
using ShagOxServer.Application.Services.Location.Regions.Translations.Create;
using ShagOxServer.Application.Services.Location.Regions.Translations.Delete;
using ShagOxServer.Application.Services.Location.Regions.Translations.Query;
using ShagOxServer.Application.Services.Location.Regions.Translations.Update;
using ShagOxServer.Application.Services.Location.Regions.Translations.Validator;

namespace ShagOxServer.Application.DependencyInjections.Location.Translations;
public static class RegionTranslationDependencyInjection
{
    public static IServiceCollection AddRegionTranslationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IRegionTranslationQueryService,
            RegionTranslationQueryService>();

        services.AddScoped<IRegionTranslationCreateService,
            RegionTranslationCreateService>();

        services.AddScoped<IRegionTranslationDeleteService,
            RegionTranslationDeleteService>();

        services.AddScoped<IRegionTranslationUpdateService,
            RegionTranslationUpdateService>();

        services.AddScoped<RegionTranslationValidator>();

        return services;
    }
}