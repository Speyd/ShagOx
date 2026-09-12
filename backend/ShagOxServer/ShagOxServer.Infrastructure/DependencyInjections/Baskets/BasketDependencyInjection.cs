using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core;

namespace ShagOxServer.Infrastructure.DependencyInjections.Baskets;
public static class BasketDependencyInjection
{
    public static IServiceCollection AddBasketInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IBasketQueryRepository,
            BasketQueryRepository>();

        services.AddScoped<IBasketExistsRepository,
            BasketExistsRepository>();

        services.AddBasketAttributeInfrastructure();

        return services;
    }
}