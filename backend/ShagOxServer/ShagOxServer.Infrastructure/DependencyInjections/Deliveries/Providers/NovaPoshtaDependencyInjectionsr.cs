using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Providers.NovaPoshta;
using ShagOxServer.Infrastructure.ExternalServices.Deliveries.Providers.NovaPoshta;

namespace ShagOxServer.Infrastructure.DependencyInjections.Deliveries.Providers;
public static class NovaPoshtaDependencyInjectionsr
{
    public static IServiceCollection AddNovaPoshtaInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<INovaPoshtaTypeResolver,
            NovaPoshtaTypeResolver>();

        return services;
    }
}
