using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.LineItems.BasketAttributes.Create;
using ShagOxServer.Application.Interfaces.Services.LineItems.BasketAttributes.Delete;
using ShagOxServer.Application.Interfaces.Services.LineItems.BasketAttributes.Query;
using ShagOxServer.Application.Interfaces.Services.LineItems.BasketAttributes.Update;
using ShagOxServer.Application.Services.LineItems.LineItemAttributes.Create;
using ShagOxServer.Application.Services.LineItems.LineItemAttributes.Delete;
using ShagOxServer.Application.Services.LineItems.LineItemAttributes.Query;
using ShagOxServer.Application.Services.LineItems.LineItemAttributes.Update;
using ShagOxServer.Application.Services.LineItems.LineItemAttributes.Update.Validator;
using ShagOxServer.Application.Services.LineItems.LineItemAttributes.Validator;

namespace ShagOxServer.Application.DependencyInjections.Baskets;
public static class BasketAttributeDependencyInjection
{
    public static IServiceCollection AddBasketAttributeApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ILineItemAttributeQueryService, 
            LineItemAttributeQueryService>();

        services.AddScoped<ILineItemAttributeCreateService,
            LineItemAttributeCreateService>();

        services.AddScoped<ILineItemAttributeUpdateService,
            LineItemAttributeUpdateService>();

        services.AddScoped<ILineItemAttributeDeleteService,
         LineItemAttributeDeleteService>();

        services.AddScoped<LineItemAttributeValidator>();

        services.AddScoped<LineItemAttributeUpdateValidator>();


        return services;
    }
}
