using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Update;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Create;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Delete;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Query;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Update;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Validator;

namespace ShagOxServer.Application.DependencyInjections.Dictionaries.Translations.Attributes;
public static class AttributeDefinitionTranslationDependencyInjection
{
    public static IServiceCollection AddAttributeTranslationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAttributeDefinitionTranslationQueryService,
            AttributeDefinitionTranslationQueryService>();

        services.AddScoped<IAttributeDefinitionTranslationCreateService,
            AttributeDefinitionTranslationCreateService>();

        services.AddScoped<IAttributeDefinitionTranslationDeleteService,
            AttributeDefinitionTranslationDeleteService>();

        services.AddScoped<IAttributeDefinitionTranslationUpdateService,
            AttributeDefinitionTranslationUpdateService>();

        services.AddScoped<AttributeDefinitionTranslationValidator>();

        return services;
    }
}