using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Create;
using ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Delete;
using ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Query;
using ShagOxServer.Application.Services.Orders.OrderItems.Create;
using ShagOxServer.Application.Services.Orders.OrderItems.Delete;
using ShagOxServer.Application.Services.Orders.OrderItems.Query;
using ShagOxServer.Application.Services.Orders.OrderItems.Validator;

namespace ShagOxServer.Application.DependencyInjections.Orders;
public static class OrderItemDependencyInjection
{
    public static IServiceCollection AddOrderItemApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IOrderItemQueryService,
           OrderItemQueryService>();

        services.AddScoped<IOrderItemCreateService,
            OrderItemCreateService>();

        //services.AddScoped<IBasketItemUpdateService,
        //    BasketItemUpdateService>();

        services.AddScoped<IOrderItemDeleteService,
            OrderItemDeleteService>();

        services.AddScoped<OrderItemValidator>();

        return services;
    }
}
