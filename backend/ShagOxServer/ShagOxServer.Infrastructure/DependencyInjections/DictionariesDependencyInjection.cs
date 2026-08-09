using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.AttributeDefinitions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes;

namespace ShagOxServer.Infrastructure.DependencyInjections;
public static class DictionariesDependencyInjection
{
    public static IServiceCollection AddDictionariesInfrastructure(this IServiceCollection services)
    {
        // ProductType
        services.AddScoped<IProductTypeQueryRepository, ProductTypeQueryRepository>();
        services.AddScoped<IProductTypeExistsRepository, ProductTypeExistsRepository>();

        // Category
        services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();
        services.AddScoped<ICategoryExistsRepository, CategoryExistsRepository>();

        // AttributeDefinition
        services.AddScoped<IAttributeDefinitionQueryRepository, AttributeDefinitionQueryRepository>();
        services.AddScoped<IAttributeDefinitionExistsRepository, AttributeDefinitionExistsRepository>();

        return services;
    }
}