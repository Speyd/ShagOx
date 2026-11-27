using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Delete;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Update;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Baskets.Core.Create;
using ShagOxServer.Application.Services.Baskets.Core.Delete;
using ShagOxServer.Application.Services.Baskets.Core.Query;
using ShagOxServer.Application.Services.Baskets.Core.Update;
using ShagOxServer.Application.Services.Baskets.Core.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Auth;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets.BasketItems;
using ShagOxServer.Domain.Cache.Baskets;
using ShagOxServer.Domain.Cache.LineItems;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.DependencyInjections.Baskets;
public static class BasketDependencyInjection
{
    public static IServiceCollection AddBasketApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IBasketQueryService,
           BasketQueryService>();

        services.AddScoped<IBasketCreateService,
            BasketCreateService>();

        services.AddScoped<IBasketUpdateService,
            BasketUpdateService>();

        services.AddScoped<IBasketDeleteService,
            BasketDeleteService>();

        services.AddScoped<BasketValidator>();

        services.AddScoped<
            ICacheInvalidationService<Basket, BasketCacheContext>,
                BasketInvalidationService>();

        services.AddScoped<BasketInvalidationService>();

        services.AddBasketItemApplication();

        return services;
    }
}
