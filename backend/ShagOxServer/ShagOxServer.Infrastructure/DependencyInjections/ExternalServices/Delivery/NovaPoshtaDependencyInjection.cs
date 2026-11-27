using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Infrastructure.ExternalServices.Deliveries.Providers.NovaPoshta;

namespace ShagOxServer.Infrastructure.DependencyInjections.ExternalServices.Delivery;
public static class NovaPoshtaDependencyInjection
{
    public static IServiceCollection AddNovaPoshtaInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<NovaPoshtaDeliveryClient>();

        services.AddScoped<IDeliveryProviderClient>(sp =>
            sp.GetRequiredService<NovaPoshtaDeliveryClient>());

        return services;
    }
}
