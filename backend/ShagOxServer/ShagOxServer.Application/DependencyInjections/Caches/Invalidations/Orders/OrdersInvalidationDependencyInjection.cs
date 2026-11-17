using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Orders.Localized;
using ShagOxServer.Application.Services.Caches.Invalidations.Orders;
using ShagOxServer.Application.Services.Caches.Invalidations.Orders.OrderItems;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.Orders;
public static class OrdersInvalidationDependencyInjection
{
    public static IServiceCollection AddOrdersInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<OrderInvalidationService>();

        services.AddScoped<OrderItemInvalidationService>();

        services.AddScoped<OrderItemOrderInvalidationService>();

        services.AddOrdersLocalizedInvalidationApplication();


        return services;
    }
}
