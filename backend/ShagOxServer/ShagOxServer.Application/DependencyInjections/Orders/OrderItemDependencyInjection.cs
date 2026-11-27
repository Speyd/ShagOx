using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Create;
using ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Delete;
using ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Query;
using ShagOxServer.Application.Services.Caches.Invalidations.Location;
using ShagOxServer.Application.Services.Caches.Invalidations.Orders.OrderItems;
using ShagOxServer.Application.Services.Orders.OrderItems.Create;
using ShagOxServer.Application.Services.Orders.OrderItems.Delete;
using ShagOxServer.Application.Services.Orders.OrderItems.Query;
using ShagOxServer.Application.Services.Orders.OrderItems.Validator;
using ShagOxServer.Domain.Cache.LineItems;
using ShagOxServer.Domain.Cache.Location;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Entities.Orders;

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

        services.AddScoped<
            ICacheInvalidationService<OrderItem, LineItemCacheContext>,
            OrderItemInvalidationService>();

        services.AddScoped<
            ICacheInvalidationService<OrderItem, LineItemCacheContext>,
            OrderItemOrderInvalidationService>();

        services.AddScoped<IOrderItemDeleteService,
            OrderItemDeleteService>();

        services.AddScoped<OrderItemDeleteService>();

        services.AddScoped<OrderItemValidator>();

        return services;
    }
}
