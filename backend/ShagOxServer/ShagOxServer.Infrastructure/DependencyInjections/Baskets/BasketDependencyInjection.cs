using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Infrastructure.DependencyInjections.Baskets;
public static class BasketDependencyInjection
{
    public static IServiceCollection AddBasketInfrastructure(
        this IServiceCollection services)
    {
        services.AddBasketAttributeInfrastructure();

        return services;
    }
}