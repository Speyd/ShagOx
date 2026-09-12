using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Query;
using ShagOxServer.Application.Services.Baskets.Core.Create;
using ShagOxServer.Application.Services.Baskets.Core.Query;
using ShagOxServer.Application.Services.Baskets.Core.Validator;

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

        //services.AddScoped<IBasketAttributeUpdateService,
        //    BasketAttributeUpdateService>();

        //services.AddScoped<IBasketAttributeDeleteService,
        // BasketAttributeDeleteService>();

        services.AddScoped<BasketValidator>();

        services.AddBasketAttributeApplication();

        return services;
    }
}