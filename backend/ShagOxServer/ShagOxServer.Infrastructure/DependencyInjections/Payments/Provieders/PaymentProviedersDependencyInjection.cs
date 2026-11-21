using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Infrastructure.DependencyInjections.Payments.Provieders;
public static class PaymentProviedersDependencyInjection
{
    public static IServiceCollection AddPaymentProviedersInfrastructure(
        this IServiceCollection services)
    {
        services.AddMonoBankInfrastructure();

        return services;
    }
}
