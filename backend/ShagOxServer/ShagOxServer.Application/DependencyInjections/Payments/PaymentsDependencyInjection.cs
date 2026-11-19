using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Payments;
using ShagOxServer.Application.Services.Payment;
using ShagOxServer.Application.Services.Payments;

namespace ShagOxServer.Application.DependencyInjections.Payments;
public static class PaymentsDependencyInjection
{
    public static IServiceCollection AddPaymentsApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IPaymentProcessingService, 
            PaymentProcessingService>();

        services.AddScoped<IPaymentWebhookProcessor, 
            PaymentWebhookProcessor>();

        return services;
    }
}
