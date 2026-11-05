using ShagOxServer.Api.Providers;
using ShagOxServer.Application.Common.Settings.Currencies;
using ShagOxServer.Application.Interfaces.Providers;

namespace ShagOxServer.Api.DependencyInjection.Currencies;
public static class CurrencyExtensions
{
    public static IServiceCollection AddCurrency(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.Configure<CurrencyRatesUpdaterSettings>(
            config.GetSection("CurrencyRatesUpdaterSettings"));

        services.AddScoped<ICurrencyProvider, 
            CurrencyProvider>();

        services.AddFx(config);

        return services;
    }
}
