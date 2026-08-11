using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Create;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Delete;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Images;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Update;
using ShagOxServer.Application.Services.Advertisements.Create;
using ShagOxServer.Application.Services.Advertisements.Create.Validator;
using ShagOxServer.Application.Services.Advertisements.Delete;
using ShagOxServer.Application.Services.Advertisements.Images;
using ShagOxServer.Application.Services.Advertisements.Query;
using ShagOxServer.Application.Services.Advertisements.Update;
using ShagOxServer.Application.Services.Advertisements.Update.Validator;
using ShagOxServer.Application.Services.Advertisements.Validator;

namespace ShagOxServer.Application.DependencyInjections.Advertisement;
public static class AdvertisementDependencyInjection
{
    public static IServiceCollection AddAdvertisementApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAdvertisementCreateService, AdvertisementCreateService>();
        services.AddScoped<IAdvertisementDeleteService, AdvertisementDeleteService>();
        services.AddScoped<IAdvertisementQueryService, AdvertisementQueryService>();
        services.AddScoped<IAdvertisementUpdateService, AdvertisementUpdateService>();

        services.AddScoped<AdvertisementCreateValidator>();
        services.AddScoped<AdvertisementUpdateValidator>();
        services.AddScoped<AdvertisementValidator>();

        services.AddScoped<IAdvertisementImageService, AdvertisementImageService>();


        services.AddFavoriteApplication();

        services.AddStatusApplication();

        return services;
    }
}