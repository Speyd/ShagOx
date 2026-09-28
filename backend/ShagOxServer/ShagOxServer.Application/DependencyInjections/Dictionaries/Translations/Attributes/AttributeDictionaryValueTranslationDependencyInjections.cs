using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Query;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Create;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Delete;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Query;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Validator;

namespace ShagOxServer.Application.DependencyInjections.Dictionaries.Translations.Attributes;
public static class AttributeDictionaryValueTranslationDependencyInjections
{
    public static IServiceCollection AddAttributeValueTranslationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAttributeDictionaryValueTranslationQueryService,
            AttributeDictionaryValueTranslationQueryService>();

        services.AddScoped<IAttributeDictionaryValueTranslationCreateService,
            AttributeDictionaryValueTranslationCreateService>();

        services.AddScoped<IAttributeDictionaryValueTranslationDeleteService,
            AttributeDictionaryValueTranslationDeleteService>();

        //services.AddScoped<IAttributeDefinitionTranslationUpdateService,
        //    AttributeDefinitionTranslationUpdateService>();

        services.AddScoped<AttributeDictionaryValueTranslationValidator>();

        return services;
    }
}