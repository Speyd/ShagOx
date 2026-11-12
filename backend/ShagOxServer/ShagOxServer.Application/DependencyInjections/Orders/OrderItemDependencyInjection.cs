using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Query;
using ShagOxServer.Application.Services.Orders.OrderItems.Query;

namespace ShagOxServer.Application.DependencyInjections.Orders;
public static class OrderItemDependencyInjection
{
    public static IServiceCollection AddOrderItemApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IOrderItemQueryService,
           OrderItemQueryService>();

        //services.AddScoped<IBasketItemCreateService,
        //    BasketItemCreateService>();

        //services.AddScoped<IBasketItemUpdateService,
        //    BasketItemUpdateService>();

        //services.AddScoped<IBasketItemDeleteService,
        //    BasketItemDeleteService>();

        //services.AddScoped<BasketItemValidator>();

        return services;
    }
}
