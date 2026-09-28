using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Query;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Query;

namespace ShagOxServer.Application.DependencyInjections.Dictionaries.Translations.Attributes;
public static class AttributeDictionaryValueTranslationDependencyInjections
{
    public static IServiceCollection AddAttributeValueTranslationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAttributeDictionaryValueTranslationQueryService,
            AttributeDictionaryValueTranslationQueryService>();

        //services.AddScoped<IAttributeDefinitionTranslationCreateService,
        //    AttributeDefinitionTranslationCreateService>();

        //services.AddScoped<IAttributeDefinitionTranslationDeleteService,
        //    AttributeDefinitionTranslationDeleteService>();

        //services.AddScoped<IAttributeDefinitionTranslationUpdateService,
        //    AttributeDefinitionTranslationUpdateService>();

        //services.AddScoped<AttributeDefinitionTranslationValidator>();

        return services;
    }
}