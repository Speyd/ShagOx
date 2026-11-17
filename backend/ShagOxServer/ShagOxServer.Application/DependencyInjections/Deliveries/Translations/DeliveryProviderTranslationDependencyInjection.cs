using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Translations.Query;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Translations.Update;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Translations.Create;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Translations.Delete;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Translations.Query;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Translations.Update;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Translations.Validator;

namespace ShagOxServer.Application.DependencyInjections.Deliveries.Translations;
public static class DeliveryProviderTranslationDependencyInjection
{
    public static IServiceCollection AddDeliveryProviderTranslationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IDeliveryProviderTranslationQueryService,
            DeliveryProviderTranslationQueryService>();

        services.AddScoped<IDeliveryProviderTranslationCreateService,
            DeliveryProviderTranslationCreateService>();

        services.AddScoped<IDeliveryProviderTranslationDeleteService,
            DeliveryProviderTranslationDeleteService>();

        services.AddScoped<IDeliveryProviderTranslationUpdateService,
            DeliveryProviderTranslationUpdateService>();

        services.AddScoped<DeliveryProviderTranslationValidator>();

        return services;
    }
}
