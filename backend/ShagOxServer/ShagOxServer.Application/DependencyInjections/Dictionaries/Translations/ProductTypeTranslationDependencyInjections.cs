using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Update;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Translation;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Create;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Delete;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Query;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Update;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Validator;
using ShagOxServer.Domain.Cache.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.DependencyInjections.Dictionaries.Translations;
public static class ProductTypeTranslationDependencyInjections
{
    public static IServiceCollection AddProductTypeTranslationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IProductTypeTranslationQueryService,
            ProductTypeTranslationQueryService>();

        services.AddScoped<IProductTypeTranslationCreateService,
            ProductTypeTranslationCreateService>();

        services.AddScoped<IProductTypeTranslationDeleteService,
            ProductTypeTranslationDeleteService>();

        services.AddScoped<IProductTypeTranslationUpdateService,
            ProductTypeTranslationUpdateService>();

        services.AddScoped<
            ICacheInvalidationService<ProductTypeTranslation, TranslationCacheContext>,
            ProductTypeTranslationInvalidationService>();

        services.AddScoped<ProductTypeTranslationInvalidationService>();

        services.AddScoped<ProductTypeTranslationValidator>();

        return services;
    }
}
