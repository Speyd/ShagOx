using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Query;
using ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Query;

namespace ShagOxServer.Application.DependencyInjections.Dictionaries.Translations;
public static class CategoryTranslationDependencyInjections
{
    public static IServiceCollection AddCategoryTranslationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ICategoryTranslationQueryService,
            CategoryTranslationQueryService>();

        //services.AddScoped<IProductTypeTranslationCreateService,
        //    ProductTypeTranslationCreateService>();

        //services.AddScoped<IProductTypeTranslationDeleteService,
        //    ProductTypeTranslationDeleteService>();

        //services.AddScoped<IProductTypeTranslationUpdateService,
        //    ProductTypeTranslationUpdateService>();

        //services.AddScoped<ProductTypeTranslationValidator>();

        return services;
    }
}