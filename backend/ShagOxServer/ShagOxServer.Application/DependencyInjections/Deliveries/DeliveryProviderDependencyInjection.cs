using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Query;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Query;

namespace ShagOxServer.Application.DependencyInjections.Deliveries;
public static class DeliveryProviderDependencyInjection
{
    public static IServiceCollection AddDeliveryProviderApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IDeliveryProviderQueryService,
            DeliveryProviderQueryService>();

        //services.AddScoped<IStatusCreateService,
        //    StatusCreateService>();

        //services.AddScoped<IStatusDeleteService,
        //    StatusDeleteService>();

        //services.AddScoped<IStatusUpdateService,
        //    StatusUpdateService>();

        //services.AddScoped<StatusValidator>();

        //services.AddStatusTranslationApplication();

        return services;
    }
}
