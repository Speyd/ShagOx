using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Delete;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Update;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Images;
using ShagOxServer.Application.Services.Advertisements.Core.Create;
using ShagOxServer.Application.Services.Advertisements.Core.Create.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Delete;
using ShagOxServer.Application.Services.Advertisements.Core.Query;
using ShagOxServer.Application.Services.Advertisements.Core.Update;
using ShagOxServer.Application.Services.Advertisements.Core.Update.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Advertisements.Images;

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