using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Query;

namespace ShagOxServer.Application.DependencyInjections.Advertisement;
public static class AdvertisementVariantDependencyInjection
{
    public static IServiceCollection AddAdvertisementVariantApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAdvertisementVariantQueryService,
            AdvertisementVariantQueryService>();

        //services.AddScoped<IStatusCreateService,
        //    StatusCreateService>();

        //services.AddScoped<IStatusDeleteService,
        //    StatusDeleteService>();

        //services.AddScoped<IStatusUpdateService,
        //    StatusUpdateService>();

        //services.AddScoped<StatusValidator>();

        return services;
    }
}