using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Query;

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