using ShagOxServer.Api.Providers;
using ShagOxServer.Application.Common.Options.Currencies;
using ShagOxServer.Application.Interfaces.Providers.Payment;

namespace ShagOxServer.Api.DependencyInjection.Currencies;
public static class CurrencyExtensions
{
    public static IServiceCollection AddCurrency(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.Configure<CurrencyRatesUpdaterOptions>(
            config.GetSection(CurrencyRatesUpdaterOptions.SectionName));

        services.AddScoped<ICurrencyProvider, 
            CurrencyProvider>();

        services.AddFx(config);

        return services;
    }
}
