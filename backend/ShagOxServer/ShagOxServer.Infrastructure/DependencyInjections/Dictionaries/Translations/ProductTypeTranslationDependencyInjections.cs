using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes.Translations;

namespace ShagOxServer.Infrastructure.DependencyInjections.Dictionaries.Translations;
public static class ProductTypeTranslationDependencyInjections
{
    public static IServiceCollection AddProductTypeTranslationInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IProductTypeTranslationQueryRepository,
            ProductTypeTranslationQueryRepository>();

        services.AddScoped<IProductTypeTranslationExistsRepository,
            ProductTypeTranslationExistsRepository>();

        return services;
    }
}