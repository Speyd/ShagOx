using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Update;
using ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Create;
using ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Delete;
using ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Query;
using ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Update;
using ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Validator;

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

        services.AddScoped<CategoryTranslationValidator>();

        return services;
    }
}