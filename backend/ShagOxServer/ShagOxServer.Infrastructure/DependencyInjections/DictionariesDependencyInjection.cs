using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries.AttributeDefinitions;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries.Categories;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.AttributeDefinitions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories;

namespace ShagOxServer.Infrastructure.DependencyInjections;
public static class DictionariesDependencyInjection
{
    public static IServiceCollection AddDictionariesInfrastructure(this IServiceCollection services)
    {
        // Category
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();
        services.AddScoped<ICategoryExistsRepository, CategoryExistsRepository>();

        // AttributeDefinition
        services.AddScoped<IAttributeDefinitionRepository, AttributeDefinitionRepository>();
        services.AddScoped<IAttributeDefinitionQueryRepository, AttributeDefinitionQueryRepository>();
        services.AddScoped<IAttributeDefinitionExistsRepository, AttributeDefinitionExistsRepository>();

        return services;
    }
}