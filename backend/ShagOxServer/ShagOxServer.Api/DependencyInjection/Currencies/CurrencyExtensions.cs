using ShagOxServer.Api.Providers;
using ShagOxServer.Application.Common.Options.Currencies;
using ShagOxServer.Application.Interfaces.Providers;

namespace ShagOxServer.Api.DependencyInjection.Currencies;
public static class CurrencyExtensions
{
    public static IServiceCollection AddCurrency(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.Configure<CurrencyRatesUpdaterOptions>(
            config.GetSection("CurrencyRatesUpdaterSettings"));

        services.AddScoped<ICurrencyProvider, 
            CurrencyProvider>();

        services.AddFx(config);

        return services;
    }
}
