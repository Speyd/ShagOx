using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Infrastructure.DependencyInjections.ExternalServices.Delivery;
public static class DeliveryDependencyInjection
{
    public static IServiceCollection AddDeliveryInfrastructure(
        this IServiceCollection services)
    {
        services.AddNovaPoshtaInfrastructure();

        return services;
    }
}
