using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Payments.PaymentWebhooks;
using ShagOxServer.Infrastructure.Persistence.Repositories.Payments.PaymentWebhooks;

namespace ShagOxServer.Infrastructure.DependencyInjections.Payments;
public static class PaymentWebhookDependencyInjection
{
    public static IServiceCollection AddPaymentWebhookInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IPaymentWebhookQueryRepository,
            PaymentWebhookQueryRepository>();

        services.AddScoped<IPaymentWebhookCommandRepository,
            PaymentWebhookCommandRepository>();

        return services;
    }
}
