using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Translations.Query;

namespace ShagOxServer.Application.DependencyInjections.Deliveries.Translations;
public static class DeliveryProviderTranslationDependencyInjection
{
    public static IServiceCollection AddDeliveryProviderTranslationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IDeliveryProviderTranslationQueryService,
            DeliveryProviderTranslationQueryService>();

        //services.AddScoped<IStatusTranslationCreateService,
        //    StatusTranslationCreateService>();

        //services.AddScoped<IStatusTranslationDeleteService,
        //    StatusTranslationDeleteService>();

        //services.AddScoped<IStatusTranslationUpdateService,
        //    StatusTranslationUpdateService>();

        //services.AddScoped<StatusTranslationValidator>();

        return services;
    }
}
