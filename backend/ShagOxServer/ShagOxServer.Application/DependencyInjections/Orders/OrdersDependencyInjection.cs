using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Application.DependencyInjections.Orders;
public static class OrdersDependencyInjection
{
    public static IServiceCollection AddOrdersItemApplication(
        this IServiceCollection services)
    {
        services.AddOrderItemApplication();

        return services;
    }
}
