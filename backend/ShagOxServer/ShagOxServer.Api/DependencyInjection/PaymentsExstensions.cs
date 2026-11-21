using ShagOxServer.Application.Common.Options.Payments;
using ShagOxServer.Application.Common.Options.Payments.Providers;

namespace ShagOxServer.Api.DependencyInjection;
public static class PaymentsExstensions
{
    public static IServiceCollection AddPayments(
       this IServiceCollection services,
       IConfiguration configuration)
    {
        services.Configure<PaymentOptions>(
            configuration.GetSection(PaymentOptions.SectionName));

        var deliveryOptions = configuration
            .GetSection(PaymentOptions.SectionName)
            .Get<PaymentOptions>() ?? new();

        string providerSection = $"{PaymentOptions.SectionName}:{PaymentProvidersOptions.SectionName}";


        #region MonoBank

        if (deliveryOptions.Providers.MonoBank.Enabled)
        {
            
            services.Configure<MonoBankOptions>(
                configuration.GetSection($"{providerSection}:MonoBank"));
        }

        #endregion

        return services;
    }
}
