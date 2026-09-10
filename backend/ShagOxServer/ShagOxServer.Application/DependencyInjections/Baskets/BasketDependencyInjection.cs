using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Application.DependencyInjections.Baskets;
public static class BasketDependencyInjection
{
    public static IServiceCollection AddBasketApplication(
        this IServiceCollection services)
    {
        services.AddBasketAttributeApplication();

        return services;
    }
}