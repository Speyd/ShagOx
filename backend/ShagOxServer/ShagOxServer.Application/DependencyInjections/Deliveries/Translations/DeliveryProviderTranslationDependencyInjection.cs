using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Translations.Query;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Translations.Create;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Translations.Query;
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

        //services.AddScoped<IStatusTranslationDeleteService,
        //    StatusTranslationDeleteService>();

        //services.AddScoped<IStatusTranslationUpdateService,
        //    StatusTranslationUpdateService>();

        services.AddScoped<DeliveryProviderTranslationValidator>();

        return services;
    }
}
