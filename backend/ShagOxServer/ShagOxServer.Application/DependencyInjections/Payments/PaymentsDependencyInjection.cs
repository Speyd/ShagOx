using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Payments;
using ShagOxServer.Application.Services.Payments;
using ShagOxServer.Application.Services.Payments.PaymentProcessing;

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
