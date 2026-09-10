using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Translations;

namespace ShagOxServer.Infrastructure.DependencyInjections.Dictionaries.Translations;
public static class CategoryTranslationDependencyInjections
{
    public static IServiceCollection AddCategoryTranslationInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<ICategoryTranslationQueryRepository,
            CategoryTranslationQueryRepository>();

        services.AddScoped<ICategoryTranslationExistsRepository,
            CategoryTranslationExistsRepository>();

        return services;
    }
}