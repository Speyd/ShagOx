using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Common.Currencies;
using ShagOxServer.Infrastructure.BackgroundServices.Currencies;
using ShagOxServer.Infrastructure.ExternalServices.Currencies;

namespace ShagOxServer.Infrastructure.DependencyInjections.ExternalServices;
public static class CurrenciesDependencyInjection
{
    public static IServiceCollection AddCurrenciesInfrastructure(
        this IServiceCollection services)
    {
        services.AddSingleton<CurrencyRateStore>();

        services.AddScoped<ICurrencyConverter, 
            CurrencyConverter>();

        services.AddHostedService<CurrencyBackgroundService>();

        return services;
    }
}
