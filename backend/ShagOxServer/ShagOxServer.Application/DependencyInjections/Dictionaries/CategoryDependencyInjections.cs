using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Dictionaries.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Update;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes;
using ShagOxServer.Application.Services.Dictionaries.Categories.Create;
using ShagOxServer.Application.Services.Dictionaries.Categories.Delete;
using ShagOxServer.Application.Services.Dictionaries.Categories.Query;
using ShagOxServer.Application.Services.Dictionaries.Categories.Update;
using ShagOxServer.Application.Services.Dictionaries.Categories.Update.Validator;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
using ShagOxServer.Domain.Cache.Dictionaries;
using ShagOxServer.Domain.Cache.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.DependencyInjections.Dictionaries;
public static class CategoryDependencyInjections
{
    public static IServiceCollection AddCategoryApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ICategoryQueryService,
            CategoryQueryService>();

        services.AddScoped<ICategoryCreateService,
            CategoryCreateService>();

        services.AddScoped<ICategoryUpdateService,
            CategoryUpdateService>();

        services.AddScoped<ICategoryDeleteService,
            CategoryDeleteService>();

        services.AddScoped<CategoryValidator>();
        services.AddScoped<CategoryUpdateValidator>();

        services.AddScoped<
            ICacheInvalidationService<Category, CategoryCacheContext>,
            CategoryInvalidationService>();

        services.AddCategoryTranslationApplication();

        return services;
    }
}
