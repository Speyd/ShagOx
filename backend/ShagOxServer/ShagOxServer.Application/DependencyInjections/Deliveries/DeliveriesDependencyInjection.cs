using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.DependencyInjections.Deliveries;
public static class DeliveriesDependencyInjection
{
    public static IServiceCollection AddDeliveriesApplication(
        this IServiceCollection services)
    {
        services.AddDeliveryProviderApplication();

        return services;
    }
}
