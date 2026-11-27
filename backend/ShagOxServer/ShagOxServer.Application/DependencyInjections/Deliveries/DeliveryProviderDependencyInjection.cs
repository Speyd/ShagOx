using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Deliveries.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Create;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Delete;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Query;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Update;
using ShagOxServer.Application.Services.Caches.Invalidations.Comments.Review;
using ShagOxServer.Application.Services.Caches.Invalidations.Deliveries;
using ShagOxServer.Application.Services.Caches.Invalidations.Deliveries.Translations;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Create;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Delete;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Query;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Update;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Validator;
using ShagOxServer.Domain.Cache.Comments;
using ShagOxServer.Domain.Cache.Deliveries;
using ShagOxServer.Domain.Entities.Comments.Review;
using ShagOxServer.Domain.Entities.Deliveries;

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

        services.AddScoped<IDeliveryProviderUpdateService,
            DeliveryProviderUpdateService>();

        services.AddScoped<DeliveryProviderValidator>();

        services.AddScoped<
            ICacheInvalidationService<DeliveryProvider, DeliveryProviderCacheContext>,
            DeliveryProviderInvalidationService>();

        services.AddScoped<DeliveryProviderInvalidationService>();

        services.AddDeliveryProviderTranslationApplication();


        return services;
    }
}
