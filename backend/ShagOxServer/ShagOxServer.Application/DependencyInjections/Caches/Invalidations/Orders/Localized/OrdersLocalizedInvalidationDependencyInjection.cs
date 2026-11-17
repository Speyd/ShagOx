using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.Orders.Localized;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Orders.Localized;
public static class OrdersLocalizedInvalidationDependencyInjection
{
    public static IServiceCollection AddOrdersLocalizedInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<OrderLocalizedInvalidationService>();

        services.AddScoped<OrderItemLocalizedInvalidationService>();


        return services;
    }
}
