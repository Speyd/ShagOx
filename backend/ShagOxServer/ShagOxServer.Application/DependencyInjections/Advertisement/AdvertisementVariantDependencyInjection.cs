using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Create;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Create;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;

namespace ShagOxServer.Application.DependencyInjections.Advertisement;
public static class AdvertisementVariantDependencyInjection
{
    public static IServiceCollection AddAdvertisementVariantApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAdvertisementVariantQueryService,
            AdvertisementVariantQueryService>();

        services.AddScoped<IAdvertisementVariantCreateService,
            AdvertisementVariantCreateService>();

        //services.AddScoped<IStatusDeleteService,
        //    StatusDeleteService>();

        //services.AddScoped<IStatusUpdateService,
        //    StatusUpdateService>();

        services.AddScoped<AdvertisementVariantValidator>();

        return services;
    }
}