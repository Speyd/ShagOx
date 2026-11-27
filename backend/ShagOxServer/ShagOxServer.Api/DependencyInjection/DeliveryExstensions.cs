using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Deliveries;
using ShagOxServer.Application.Common.Options.Deliveries.Providers;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Providers.NovaPoshta;
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

        string providerSection = $"{DeliveryOptions.SectionName}:{DeliveryProvidersOptions.SectionName}";


        #region Nova Poshta

        if (deliveryOptions.Providers.NovaPoshta.Enabled)
        {
            var novaPoshtaSection = configuration.GetSection(
                $"{providerSection}:{NovaPoshtaOptions.SectionName}");

            services.Configure<NovaPoshtaOptions>(novaPoshtaSection);

            void ConfigureNovaPoshtaClient(
                IServiceProvider sp,
                HttpClient client)
            {
                var options = sp
                    .GetRequiredService<IOptions<NovaPoshtaOptions>>()
                    .Value;

                if (string.IsNullOrWhiteSpace(options.BaseUrl))
                    throw new InvalidOperationException(
                        "NovaPoshta BaseUrl is not configured.");

                client.BaseAddress = new Uri(
                    options.BaseUrl,
                    UriKind.Absolute);
            }

            services.AddHttpClient<NovaPoshtaApiClient>(
                ConfigureNovaPoshtaClient);

            services.AddHttpClient<
                INovaPoshtaTypeResolver,
                NovaPoshtaTypeResolver>(
                    ConfigureNovaPoshtaClient);
        }
        #endregion

        return services;
    }
}
