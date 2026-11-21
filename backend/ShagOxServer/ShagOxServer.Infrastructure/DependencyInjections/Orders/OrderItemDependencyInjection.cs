using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Orders.OrderItems;
using ShagOxServer.Infrastructure.Persistence.Repositories.Orders.OrderItems;
using ShagOxServer.Infrastructure.Persistence.Repositories.Orders.OrderItems.Query;

namespace ShagOxServer.Infrastructure.DependencyInjections.Orders;
public static class OrderItemDependencyInjection
{
    public static IServiceCollection AddOrderItemInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IOrderItemQueryRepository, 
            OrderItemQueryRepository>();

        services.AddScoped<IOrderItemExistsRepository,
            OrderItemExistsRepository>();

        return services;
    }
}
