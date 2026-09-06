using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Query;
using ShagOxServer.Application.Services.Location.Cities.Translations.Validator;
using ShagOxServer.Application.Services.Specification.Conditions.Translations.Query;

namespace ShagOxServer.Application.DependencyInjections.Specification.Translations;
public static class ConditionTranslationDependencyInjection
{
    public static IServiceCollection AddConditionTranslationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IConditionTranslationQueryService,
            ConditionTranslationQueryService>();

        //services.AddScoped<ICityTranslationCreateService,
        //    CityTranslationCreateService>();

        //services.AddScoped<ICityTranslationDeleteService,
        //    CityTranslationDeleteService>();

        //services.AddScoped<ICityTranslationUpdateService,
        //    CityTranslationUpdateService>();

        services.AddScoped<CityTranslationValidator>();

        return services;
    }
}