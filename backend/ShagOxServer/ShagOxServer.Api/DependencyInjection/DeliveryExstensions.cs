using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Deliveries;
using ShagOxServer.Application.Common.Options.Deliveries.Providers;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Providers;
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

        services.Configure<NovaPoshtaOptions>(
            configuration.GetSection(
                $"{DeliveryOptions.SectionName}:Providers:NovaPoshta"));

        var deliveryOptions = configuration
            .GetSection(DeliveryOptions.SectionName)
            .Get<DeliveryOptions>() ?? new();


        #region Nova Poshta

        if (deliveryOptions.Providers.NovaPoshta.Enabled)
        {
            services.AddHttpClient<NovaPoshtaApiClient>(
                SetNovaPoshtaBaseAddress);

            services.AddHttpClient<INovaPoshtaTypeResolver, NovaPoshtaTypeResolver>(
                SetNovaPoshtaBaseAddress);

            services.AddScoped<NovaPoshtaDeliveryClient>();

            services.AddScoped<IDeliveryProviderClient>(sp =>
                sp.GetRequiredService<NovaPoshtaDeliveryClient>());
        }

        #endregion

        return services;
    }

    private static void SetNovaPoshtaBaseAddress(
        IServiceProvider serviceProvider,
        HttpClient client)
    {
        var options = serviceProvider
            .GetRequiredService<IOptions<NovaPoshtaOptions>>()
            .Value;

        client.BaseAddress = new Uri(options.BaseUrl);
    }
}
