using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Delete;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Update;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Advertisements.Core.Create;
using ShagOxServer.Application.Services.Advertisements.Core.Create.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Delete;
using ShagOxServer.Application.Services.Advertisements.Core.Query;
using ShagOxServer.Application.Services.Advertisements.Core.Update;
using ShagOxServer.Application.Services.Advertisements.Core.Update.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements.Translations;
using ShagOxServer.Domain.Cache.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.DependencyInjections.Advertisements;
public static class AdvertisementsDependencyInjection
{
    public static IServiceCollection AddAdvertisementsApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAdvertisementCreateService, AdvertisementCreateService>();
        services.AddScoped<IAdvertisementDeleteService, AdvertisementDeleteService>();
        services.AddScoped<IAdvertisementQueryService, AdvertisementQueryService>();
        services.AddScoped<IAdvertisementUpdateService, AdvertisementUpdateService>();

        services.AddScoped<AdvertisementCreateValidator>();
        services.AddScoped<AdvertisementUpdateValidator>();
        services.AddScoped<AdvertisementValidator>();

        services.AddScoped<
            BaseValidator<Advertisement>,
            AdvertisementValidator>();

        services.AddScoped<
            ICacheInvalidationService<Advertisement, AdvertisementCacheContext>,
            AdvertisementInvalidationService>();

        services.AddScoped<AdvertisementInvalidationService>();

        services.AddFavoriteApplication();

        services.AddStatusApplication();

        services.AddAdvertisementVariantApplication();


        return services;
    }
}
