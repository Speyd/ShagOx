using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Payments.Core;
using ShagOxServer.Infrastructure.DependencyInjections.Payments.Provieders;
using ShagOxServer.Infrastructure.Persistence.Repositories.Payments.Core;
using ShagOxServer.Infrastructure.Persistence.Repositories.Payments.Core.Query;

namespace ShagOxServer.Infrastructure.DependencyInjections.Payments;
public static class PaymentsDependencyInjection
{
    public static IServiceCollection AddPaymentsInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IPaymentQueryRepository,
            PaymentQueryRepository>();

        services.AddScoped<IPaymentCommandRepository,
            PaymentCommandRepository>();

        services.AddPaymentProviedersInfrastructure();

        services.AddPaymentWebhookInfrastructure();

        return services;
    }
}
