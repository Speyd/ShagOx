using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Query;
using ShagOxServer.Application.Services.Location.Regions.Translations.Create;
using ShagOxServer.Application.Services.Location.Regions.Translations.Query;
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

        //services.AddScoped<IStatusTranslationDeleteService,
        //    StatusTranslationDeleteService>();

        //services.AddScoped<IStatusTranslationUpdateService,
        //    StatusTranslationUpdateService>();

        services.AddScoped<RegionTranslationValidator>();

        return services;
    }
}