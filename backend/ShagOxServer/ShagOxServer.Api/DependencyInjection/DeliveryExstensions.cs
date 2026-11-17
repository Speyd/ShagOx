using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Deliveries;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Providers;
using ShagOxServer.Infrastructure.ExternalServices.Deliveries.Providers.NovaPoshta;

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
            void ConfigureNovaPoshtaClient(IServiceProvider sp, HttpClient client)
            {
                var options = sp
                    .GetRequiredService<IOptions<DeliveryOptions>>()
                    .Value
                    .Providers
                    .NovaPoshta;

                if (string.IsNullOrWhiteSpace(options.BaseUrl))
                    throw new InvalidOperationException(
                        "NovaPoshta BaseUrl is not configured.");

                client.BaseAddress = new Uri(options.BaseUrl);
            }

            services.AddHttpClient<NovaPoshtaApiClient>(
                ConfigureNovaPoshtaClient);

            services.AddHttpClient<NovaPoshtaTypeResolver>(
                ConfigureNovaPoshtaClient);


            services.AddScoped<NovaPoshtaDeliveryClient>();

            services.AddScoped<IDeliveryProviderClient>(sp =>
                sp.GetRequiredService<NovaPoshtaDeliveryClient>());
        }

        #endregion

        return services;
    }
}
