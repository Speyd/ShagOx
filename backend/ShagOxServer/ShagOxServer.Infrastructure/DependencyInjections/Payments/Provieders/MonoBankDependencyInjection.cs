using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Infrastructure.ExternalServices.Payments.Providers.MonoBank;

namespace ShagOxServer.Infrastructure.DependencyInjections.Payments.Provieders;
public static class MonoBankDependencyInjection
{
    public static IServiceCollection AddMonoBankInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IPaymentProvider,
            MonoBankPaymentProvider>();

        return services;
    }
}
