using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Deliveries.DeliveryProviders.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Deliveries.DeliveryProviders.Translations;

namespace ShagOxServer.Infrastructure.DependencyInjections.Deliveries.Translations;
public static class DeliveryProviderTranslationDependencyInjections
{
    public static IServiceCollection AddDeliveryProviderTranslationInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IDeliveryProviderTranslationQueryRepository,
            DeliveryProviderTranslationQueryRepository>();

        services.AddScoped<IDeliveryProviderTranslationExistsRepository,
            DeliveryProviderTranslationExistsRepository>();

        return services;
    }
}
