using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Create;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Delete;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Query;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Update;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Baskets.BasketItems.Create;
using ShagOxServer.Application.Services.Baskets.BasketItems.Delete;
using ShagOxServer.Application.Services.Baskets.BasketItems.Query;
using ShagOxServer.Application.Services.Baskets.BasketItems.Update;
using ShagOxServer.Application.Services.Baskets.BasketItems.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets.BasketItems;
using ShagOxServer.Domain.Cache.LineItems;
using ShagOxServer.Domain.Entities.Baskets;

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

        services.AddScoped<IBasketItemUpdateService,
            BasketItemUpdateService>();

        services.AddScoped<IBasketItemDeleteService,
            BasketItemDeleteService>();

        services.AddScoped<
            ICacheInvalidationService<BasketItem, LineItemCacheContext>,
            BasketItemInvalidationService>();

        services.AddScoped<
            ICacheInvalidationService<BasketItem, LineItemCacheContext>,
            BasketItemBasketInvalidationService>();

        services.AddScoped<BasketItemValidator>();

        return services;
    }
}
