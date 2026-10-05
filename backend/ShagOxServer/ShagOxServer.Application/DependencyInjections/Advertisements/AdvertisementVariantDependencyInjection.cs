using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Create;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Delete;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Create;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Delete;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;

namespace ShagOxServer.Application.DependencyInjections.Advertisements;
public static class AdvertisementVariantDependencyInjection
{
    public static IServiceCollection AddAdvertisementVariantApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAdvertisementVariantQueryService,
            AdvertisementVariantQueryService>();

        services.AddScoped<IAdvertisementVariantCreateService,
            AdvertisementVariantCreateService>();

        services.AddScoped<IAdvertisementVariantDeleteService,
            AdvertisementVariantDeleteService>();

        services.AddScoped<IAdvertisementVariantUpdateService,
            AdvertisementVariantUpdateService>();

        services.AddScoped<AdvertisementVariantValidator>();

        return services;
    }
}