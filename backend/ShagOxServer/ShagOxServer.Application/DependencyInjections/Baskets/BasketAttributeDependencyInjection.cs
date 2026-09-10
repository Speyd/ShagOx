using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Create;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Query;
using ShagOxServer.Application.Services.Baskets.BasketAttributes.Create;
using ShagOxServer.Application.Services.Baskets.BasketAttributes.Query;
using ShagOxServer.Application.Services.Baskets.BasketAttributes.Validator;

namespace ShagOxServer.Application.DependencyInjections.Baskets;
public static class BasketAttributeDependencyInjection
{
    public static IServiceCollection AddBasketAttributeApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IBasketAttributeQueryService, 
            BasketAttributeQueryService>();

        services.AddScoped<IBasketAttributeCreateService,
            BasketAttributeCreateService>();

        //services.AddScoped<IRoleUpdateService,
        //RoleUpdateService>();

        //services.AddScoped<IRoleDeleteService,
        //RoleDeleteService>();

        services.AddScoped<BasketAttributeValidator>();

        return services;
    }
}