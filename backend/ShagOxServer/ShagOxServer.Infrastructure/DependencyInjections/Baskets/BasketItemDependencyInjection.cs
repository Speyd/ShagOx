using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems;

namespace ShagOxServer.Infrastructure.DependencyInjections.Baskets;
public static class BasketItemDependencyInjection
{
    public static IServiceCollection AddBasketItemInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IBasketItemQueryRepository,
            BasketItemQueryRepository>();

        services.AddScoped<IBasketItemExistsRepository,
            BasketItemExistsRepository>();

        return services;
    }
}