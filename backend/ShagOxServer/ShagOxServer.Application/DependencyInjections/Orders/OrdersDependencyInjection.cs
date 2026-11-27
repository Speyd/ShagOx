using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Orders;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Delete;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Query;
using ShagOxServer.Application.Services.Caches.Invalidations.Location;
using ShagOxServer.Application.Services.Caches.Invalidations.Orders;
using ShagOxServer.Application.Services.Orders;
using ShagOxServer.Application.Services.Orders.Core.Create;
using ShagOxServer.Application.Services.Orders.Core.Delete;
using ShagOxServer.Application.Services.Orders.Core.Query;
using ShagOxServer.Application.Services.Orders.Core.Validator;
using ShagOxServer.Application.Services.Orders.Payment;
using ShagOxServer.Application.Services.Orders.Status;
using ShagOxServer.Domain.Cache.Orders;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.DependencyInjections.Orders;
public static class OrdersDependencyInjection
{
    public static IServiceCollection AddOrdersItemApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IOrderQueryService,
           OrderQueryService>();

        services.AddScoped<IOrderCreateService,
            OrderCreateService>();

        services.AddScoped<IOrderDeleteService,
            OrderDeleteService>();


        services.AddScoped<IOrderPaymentService,
            OrderPaymentService>();

        services.AddScoped<IOrderStatusService,
            OrderStatusService>();
        



        services.AddScoped<
            ICacheInvalidationService<Order, OrderCacheContext>,
            OrderInvalidationService>();

        services.AddScoped<OrderInvalidationService>();



        services.AddScoped<OrderValidator>();

        services.AddScoped<IOrderNumberGenerator,
           OrderNumberGenerator>();

        services.AddOrderItemApplication();

        return services;
    }
}
