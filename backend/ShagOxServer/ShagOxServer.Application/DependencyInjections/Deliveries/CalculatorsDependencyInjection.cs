using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Calculators;
using ShagOxServer.Application.Services.Deliveries.Calculators;
using ShagOxServer.Application.Services.Deliveries.Providers;

namespace ShagOxServer.Application.DependencyInjections.Deliveries;
public static class CalculatorsDependencyInjection
{
    public static IServiceCollection AddDeliveryCalculatorsApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IDeliveryPriceCalculator,
           DeliveryPriceCalculator>();

        services.AddScoped<DeliveryProviderResolver>();

        return services;
    }
}

