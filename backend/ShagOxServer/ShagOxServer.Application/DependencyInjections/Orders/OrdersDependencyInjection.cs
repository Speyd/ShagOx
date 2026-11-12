using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Query;
using ShagOxServer.Application.Services.Orders.Core.Query;

namespace ShagOxServer.Application.DependencyInjections.Orders;
public static class OrdersDependencyInjection
{
    public static IServiceCollection AddOrdersItemApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IOrderQueryService,
           OrderQueryService>();

        //services.AddScoped<IOrderItemCreateService,
        //    OrderItemCreateService>();

        //services.AddScoped<IBasketItemUpdateService,
        //    BasketItemUpdateService>();

        //services.AddScoped<IOrderItemDeleteService,
        //    OrderItemDeleteService>();

        //services.AddScoped<OrderItemValidator>();

        services.AddOrderItemApplication();

        return services;
    }
}
