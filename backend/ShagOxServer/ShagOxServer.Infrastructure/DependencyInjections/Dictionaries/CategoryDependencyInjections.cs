using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Infrastructure.DependencyInjections.Dictionaries.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories;

namespace ShagOxServer.Infrastructure.DependencyInjections.Dictionaries;
public static class CategoryDependencyInjections
{
    public static IServiceCollection AddCategoryInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();
        services.AddScoped<ICategoryExistsRepository, CategoryExistsRepository>();

        services.AddCategoryTranslationInfrastructure();

        return services;
    }
}