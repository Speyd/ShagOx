using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Translations.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Translations.Update;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Translations.Create;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Translations.Delete;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Translations.Query;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Translations.Update;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Translations.Validator;

namespace ShagOxServer.Application.DependencyInjections.Dictionaries.Translations;
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