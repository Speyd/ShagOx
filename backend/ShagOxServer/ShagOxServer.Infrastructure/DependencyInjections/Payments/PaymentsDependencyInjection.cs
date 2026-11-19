using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Infrastructure.ExternalServices.Payments.Providers;

namespace ShagOxServer.Infrastructure.DependencyInjections.Payments;
public static class PaymentsDependencyInjection
{
    public static IServiceCollection AddPaymentsInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IPaymentProvider, 
            MonoBankPaymentProvider>();

        services.AddPaymentWebhookInfrastructure();

        return services;
    }
}
