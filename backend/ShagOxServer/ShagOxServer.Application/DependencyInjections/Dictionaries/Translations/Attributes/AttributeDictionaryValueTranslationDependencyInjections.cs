using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Update;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Translation;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Create;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Delete;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Query;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Update;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Validator;
using ShagOxServer.Domain.Cache.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;

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

        services.AddScoped<IAttributeDictionaryValueTranslationUpdateService,
            AttributeDictionaryValueTranslationUpdateService>();

        services.AddScoped<
            ICacheInvalidationService<AttributeDictionaryValueTranslation, TranslationCacheContext>,
            AttributeDictionaryValueTranslationInvalidationService>();

        services.AddScoped<AttributeDictionaryValueTranslationValidator>();

        return services;
    }
}
