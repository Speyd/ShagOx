using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Infrastructure.DependencyInjections.Deliveries.Providers;
public static class PovidersDependencyInjections
{
    public static IServiceCollection AddPovidersInfrastructure(
        this IServiceCollection services)
    {
        services.AddNovaPoshtaInfrastructure();

        return services;
    }
}
