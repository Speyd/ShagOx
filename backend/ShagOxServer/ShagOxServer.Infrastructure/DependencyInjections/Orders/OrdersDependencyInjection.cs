using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Infrastructure.DependencyInjections.Orders;
public static class OrdersDependencyInjection
{
    public static IServiceCollection AddOrdersInfrastructure(
        this IServiceCollection services)
    {
        services.AddOrderItemInfrastructure();

        return services;
    }
}
