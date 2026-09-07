using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Translations.Query;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Translations.Query;

namespace ShagOxServer.Application.DependencyInjections.Dictionaries.Translations;
public static class AttributeDefinitionTranslationDependencyInjection
{
    public static IServiceCollection AddAttributeTranslationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAttributeDefinitionTranslationQueryService,
            AttributeDefinitionTranslationQueryService>();

        //services.AddScoped<ICityTranslationCreateService,
        //    CityTranslationCreateService>();

        //services.AddScoped<ICityTranslationDeleteService,
        //    CityTranslationDeleteService>();

        //services.AddScoped<ICityTranslationUpdateService,
        //    CityTranslationUpdateService>();

        //services.AddScoped<CityTranslationValidator>();

        return services;
    }
}