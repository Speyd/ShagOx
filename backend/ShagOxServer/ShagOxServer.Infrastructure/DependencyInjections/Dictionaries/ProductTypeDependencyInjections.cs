using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes;

namespace ShagOxServer.Infrastructure.DependencyInjections.Dictionaries;
public static class ProductTypeDependencyInjections
{
    public static IServiceCollection AddProductTypeInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IProductTypeQueryRepository, ProductTypeQueryRepository>();
        services.AddScoped<IProductTypeExistsRepository, ProductTypeExistsRepository>();

        return services;
    }
}