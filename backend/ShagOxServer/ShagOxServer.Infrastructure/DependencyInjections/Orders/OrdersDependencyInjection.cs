using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Infrastructure.Persistence.Repositories.Orders.Core;
using ShagOxServer.Infrastructure.Persistence.Repositories.Orders.Core.Query;

namespace ShagOxServer.Infrastructure.DependencyInjections.Orders;
public static class OrdersDependencyInjection
{
    public static IServiceCollection AddOrdersInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IOrderQueryRepository,
            OrderQueryRepository>();

        services.AddScoped<IOrderExistsRepository,
            OrderExistsRepository>();

        services.AddOrderItemInfrastructure();

        return services;
    }
}
