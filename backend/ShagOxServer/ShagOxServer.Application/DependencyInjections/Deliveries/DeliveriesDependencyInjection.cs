using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Queries;
using ShagOxServer.Application.Services.Deliveries;

namespace ShagOxServer.Application.DependencyInjections.Deliveries;
public static class DeliveriesDependencyInjection
{
    public static IServiceCollection AddDeliveriesApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IDeliveryQueriesService,
            DeliveryQueriesService>();

        services.AddDeliveryProviderApplication();

        services.AddDeliveryCalculatorsApplication();

        return services;
    }
}
