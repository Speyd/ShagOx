using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Create;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Query;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Create;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Delete;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Query;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Validator;

namespace ShagOxServer.Application.DependencyInjections.Deliveries;
public static class DeliveryProviderDependencyInjection
{
    public static IServiceCollection AddDeliveryProviderApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IDeliveryProviderQueryService,
            DeliveryProviderQueryService>();

        services.AddScoped<IDeliveryProviderCreateService,
            DeliveryProviderCreateService>();

        services.AddScoped<IDeliveryProviderDeleteService,
            DeliveryProviderDeleteService>();

        //services.AddScoped<IStatusUpdateService,
        //    StatusUpdateService>();

        services.AddScoped<DeliveryProviderValidator>();

        //services.AddStatusTranslationApplication();

        return services;
    }
}
