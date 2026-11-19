using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Orders;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Delete;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Query;
using ShagOxServer.Application.Services.Orders;
using ShagOxServer.Application.Services.Orders.Core.Create;
using ShagOxServer.Application.Services.Orders.Core.Delete;
using ShagOxServer.Application.Services.Orders.Core.Query;
using ShagOxServer.Application.Services.Orders.Core.Validator;

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

        //services.AddScoped<IBasketItemUpdateService,
        //    BasketItemUpdateService>();

        services.AddScoped<IOrderDeleteService,
            OrderDeleteService>();

        services.AddScoped<OrderValidator>();

        services.AddScoped<IOrderNumberGenerator,
           OrderNumberGenerator>();

        services.AddOrderItemApplication();

        return services;
    }
}
