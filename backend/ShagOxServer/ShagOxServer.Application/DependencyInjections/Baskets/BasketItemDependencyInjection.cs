using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Create;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Delete;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Query;
using ShagOxServer.Application.Services.Baskets.BasketItems.Create;
using ShagOxServer.Application.Services.Baskets.BasketItems.Delete;
using ShagOxServer.Application.Services.Baskets.BasketItems.Query;
using ShagOxServer.Application.Services.Baskets.BasketItems.Validator;

namespace ShagOxServer.Application.DependencyInjections.Baskets;
public static class BasketItemDependencyInjection
{
    public static IServiceCollection AddBasketItemApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IBasketItemQueryService,
           BasketItemQueryService>();

        services.AddScoped<IBasketItemCreateService,
            BasketItemCreateService>();

        //services.AddScoped<IBasketUpdateService,
        //    BasketUpdateService>();

        services.AddScoped<IBasketItemDeleteService,
            BasketItemDeleteService>();

        services.AddScoped<BasketItemValidator>();

        return services;
    }
}