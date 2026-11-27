using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Update;
using ShagOxServer.Application.Services.Caches.Invalidations.Deliveries.Translations;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Translation;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Create;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Delete;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Query;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Update;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Validator;
using ShagOxServer.Domain.Cache.Translations;
using ShagOxServer.Domain.Entities.Deliveries.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;

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

        services.AddScoped<
            ICacheInvalidationService<AttributeDefinitionTranslation, TranslationCacheContext>,
            AttributeDefinitionTranslationInvalidationService>();

        services.AddScoped<AttributeDefinitionTranslationInvalidationService>();

        services.AddScoped<AttributeDefinitionTranslationValidator>();

        return services;
    }
}
