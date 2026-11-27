using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Update;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Translation;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Translation;
using ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Create;
using ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Delete;
using ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Query;
using ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Update;
using ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Validator;
using ShagOxServer.Domain.Cache.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.DependencyInjections.Dictionaries.Translations;
public static class CategoryTranslationDependencyInjections
{
    public static IServiceCollection AddCategoryTranslationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ICategoryTranslationQueryService,
            CategoryTranslationQueryService>();

        services.AddScoped<ICategoryTranslationCreateService,
            CategoryTranslationCreateService>();

        services.AddScoped<ICategoryTranslationDeleteService,
            CategoryTranslationDeleteService>();

        services.AddScoped<ICategoryTranslationUpdateService,
            CategoryTranslationUpdateService>();

        services.AddScoped<
            ICacheInvalidationService<CategoryTranslation, TranslationCacheContext>,
            CategoryTranslationInvalidationService>();

        services.AddScoped<CategoryTranslationInvalidationService>();

        services.AddScoped<CategoryTranslationValidator>();

        return services;
    }
}
