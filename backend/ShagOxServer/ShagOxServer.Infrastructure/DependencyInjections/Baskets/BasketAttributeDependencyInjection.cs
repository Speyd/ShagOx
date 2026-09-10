using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes;

namespace ShagOxServer.Infrastructure.DependencyInjections.Baskets;
public static class BasketAttributeDependencyInjection
{
    public static IServiceCollection AddBasketAttributeInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IBasketAttributeQueryRepository, 
            BasketAttributeQueryRepository>();

        services.AddScoped<IBasketAttributeExistsRepository,
            BasketAttributeExistsRepository>();

        return services;
    }
}