using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Deliveries;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Calculators;
using ShagOxServer.Application.Services.Deliveries.Providers.NovaPoshta;

namespace ShagOxServer.Api.DependencyInjection;
public static class DeliveryExstensions
{
    public static IServiceCollection AddDelivery(
       this IServiceCollection services,
       IConfiguration configuration)
    {
        services.Configure<DeliveryOptions>(
            configuration.GetSection(DeliveryOptions.SectionName));

        var deliveryOptions = configuration
            .GetSection(DeliveryOptions.SectionName)
            .Get<DeliveryOptions>() ?? new();


        #region Nova Poshta

        if (deliveryOptions.Providers.NovaPoshta.Enabled)
        {
            services.AddHttpClient<NovaPoshtaDeliveryClient>(
            (sp, client) =>
            {
                var options = sp
                    .GetRequiredService<IOptions<DeliveryOptions>>()
                    .Value
                    .Providers
                    .NovaPoshta;

                client.BaseAddress = new Uri(options.BaseUrl);
            });

            services.AddScoped<IDeliveryProviderClient>(sp =>
                sp.GetRequiredService<NovaPoshtaDeliveryClient>());
        }

        #endregion
        return services;
    }
}
