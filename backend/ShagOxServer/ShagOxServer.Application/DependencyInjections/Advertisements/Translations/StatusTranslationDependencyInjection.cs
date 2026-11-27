using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Update;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Create;
using ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Delete;
using ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Query;
using ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Update;
using ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements.Translations;
using ShagOxServer.Application.Services.Caches.Invalidations.Orders.OrderItems;
using ShagOxServer.Domain.Cache.LineItems;
using ShagOxServer.Domain.Cache.Translations;
using ShagOxServer.Domain.Entities.Advertisements.Translations;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.DependencyInjections.Advertisements.Translations;
public static class StatusTranslationDependencyInjection
{
    public static IServiceCollection AddStatusTranslationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IStatusTranslationQueryService, 
            StatusTranslationQueryService>();

        services.AddScoped<IStatusTranslationCreateService,
            StatusTranslationCreateService>();

        services.AddScoped<IStatusTranslationDeleteService,
            StatusTranslationDeleteService>();

        services.AddScoped<IStatusTranslationUpdateService,
            StatusTranslationUpdateService>();

        services.AddScoped<
            ICacheInvalidationService<StatusTranslation, TranslationCacheContext>,
            StatusTranslationInvalidationService>();

        services.AddScoped<StatusTranslationInvalidationService>();

        services.AddScoped<StatusTranslationValidator>();

        return services;
    }
}
