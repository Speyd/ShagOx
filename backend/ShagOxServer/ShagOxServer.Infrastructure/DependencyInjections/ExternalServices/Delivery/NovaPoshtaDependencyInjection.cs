using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Providers.NovaPoshta;
using ShagOxServer.Infrastructure.ExternalServices.Deliveries.Providers.NovaPoshta;

namespace ShagOxServer.Infrastructure.DependencyInjections.ExternalServices.Delivery;
public static class NovaPoshtaDependencyInjection
{
    public static IServiceCollection AddNovaPoshtaInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<INovaPoshtaTypeResolver,
            NovaPoshtaTypeResolver>();

        return services;
    }
}
