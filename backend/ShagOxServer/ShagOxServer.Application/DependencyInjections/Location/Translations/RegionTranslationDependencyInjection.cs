using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Query;
using ShagOxServer.Application.Services.Location.Regions.Translations.Query;

namespace ShagOxServer.Application.DependencyInjections.Location.Translations;
public static class RegionTranslationDependencyInjection
{
    public static IServiceCollection AddRegionTranslationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IRegionTranslationQueryService,
            RegionTranslationQueryService>();

        //services.AddScoped<IStatusTranslationCreateService,
        //    StatusTranslationCreateService>();

        //services.AddScoped<IStatusTranslationDeleteService,
        //    StatusTranslationDeleteService>();

        //services.AddScoped<IStatusTranslationUpdateService,
        //    StatusTranslationUpdateService>();

        //services.AddScoped<StatusTranslationValidator>();

        return services;
    }
}