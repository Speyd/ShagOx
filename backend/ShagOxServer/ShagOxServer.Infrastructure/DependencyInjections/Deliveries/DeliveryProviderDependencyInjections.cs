using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Deliveries.DeliveryProviders;
using ShagOxServer.Infrastructure.Persistence.Repositories.Deliveries.DeliveryProviders;
using ShagOxServer.Infrastructure.Persistence.Repositories.Deliveries.DeliveryProviders.Query;

namespace ShagOxServer.Infrastructure.DependencyInjections.Deliveries;
public static class DeliveryProviderDependencyInjections
{
    public static IServiceCollection AddDeliveryProviderInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IDeliveryProviderQueryRepository,
            DeliveryProviderQueryRepository>();

        services.AddScoped<IDeliveryProviderExistsRepository,
            DeliveryProviderExistsRepository>();

        //services.AddCategoryTranslationInfrastructure();

        return services;
    }
}
