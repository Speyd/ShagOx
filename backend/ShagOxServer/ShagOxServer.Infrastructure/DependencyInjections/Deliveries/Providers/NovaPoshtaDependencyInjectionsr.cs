using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Providers.NovaPoshta;
using ShagOxServer.Infrastructure.ExternalServices.Deliveries.Providers.NovaPoshta;

namespace ShagOxServer.Infrastructure.DependencyInjections.Deliveries.Providers;
public static class NovaPoshtaDependencyInjectionsr
{
    public static IServiceCollection AddNovaPoshtaInfrastructure(
        this IServiceCollection services)
    {
        services.TryAddScoped<INovaPoshtaTypeResolver,
            NovaPoshtaTypeResolver>();

        return services;
    }
}
