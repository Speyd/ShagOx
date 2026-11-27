using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Update;
using ShagOxServer.Application.Services.Caches.Invalidations.Location.Translation;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Translations;
using ShagOxServer.Application.Services.Specification.Conditions.Translations.Create;
using ShagOxServer.Application.Services.Specification.Conditions.Translations.Delete;
using ShagOxServer.Application.Services.Specification.Conditions.Translations.Query;
using ShagOxServer.Application.Services.Specification.Conditions.Translations.Update;
using ShagOxServer.Application.Services.Specification.Conditions.Translations.Validator;
using ShagOxServer.Domain.Cache.Translations;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.Domain.Entities.Specification.Translations;

namespace ShagOxServer.Application.DependencyInjections.Specification.Translations;
public static class ConditionTranslationDependencyInjection
{
    public static IServiceCollection AddConditionTranslationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IConditionTranslationQueryService,
            ConditionTranslationQueryService>();

        services.AddScoped<IConditionTranslationCreateService,
            ConditionTranslationCreateService>();

        services.AddScoped<IConditionTranslationDeleteService,
            ConditionTranslationDeleteService>();

        services.AddScoped<IConditionTranslationUpdateService,
            ConditionTranslationUpdateService>();

        services.AddScoped<
            ICacheInvalidationService<ConditionTranslation, TranslationCacheContext>,
            ConditionTranslationInvalidationService>();

        services.AddScoped<ConditionTranslationInvalidationService>();

        services.AddScoped<ConditionTranslationValidator>();

        return services;
    }
}
