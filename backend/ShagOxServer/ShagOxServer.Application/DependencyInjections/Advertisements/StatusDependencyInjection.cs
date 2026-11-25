using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Advertisements.Translations;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Create;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Delete;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Update;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Advertisements.Statuses.Create;
using ShagOxServer.Application.Services.Advertisements.Statuses.Delete;
using ShagOxServer.Application.Services.Advertisements.Statuses.Query;
using ShagOxServer.Application.Services.Advertisements.Statuses.Update;
using ShagOxServer.Application.Services.Advertisements.Statuses.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Domain.Cache.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.DependencyInjections.Advertisements;
public static class StatusDependencyInjection
{
    public static IServiceCollection AddStatusApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IStatusQueryService,
            StatusQueryService>();

        services.AddScoped<IStatusCreateService, 
            StatusCreateService>();

        services.AddScoped<IStatusDeleteService,
            StatusDeleteService>();

        services.AddScoped<IStatusUpdateService,
            StatusUpdateService>();

        services.AddScoped<
            ICacheInvalidationService<Status, StatusCacheContext>,
            StatusInvalidationService>();

        services.AddScoped<StatusValidator>();

        services.AddStatusTranslationApplication();

        return services;
    }
}
