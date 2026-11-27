using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.LineItems.BasketAttributes.Create;
using ShagOxServer.Application.Interfaces.Services.LineItems.BasketAttributes.Delete;
using ShagOxServer.Application.Interfaces.Services.LineItems.BasketAttributes.Query;
using ShagOxServer.Application.Interfaces.Services.LineItems.BasketAttributes.Update;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries;
using ShagOxServer.Application.Services.Caches.Invalidations.LineItems;
using ShagOxServer.Application.Services.LineItems.LineItemAttributes.Create;
using ShagOxServer.Application.Services.LineItems.LineItemAttributes.Delete;
using ShagOxServer.Application.Services.LineItems.LineItemAttributes.Query;
using ShagOxServer.Application.Services.LineItems.LineItemAttributes.Update;
using ShagOxServer.Application.Services.LineItems.LineItemAttributes.Update.Validator;
using ShagOxServer.Application.Services.LineItems.LineItemAttributes.Validator;
using ShagOxServer.Domain.Cache.LineItems;
using ShagOxServer.Domain.Entities.LineItems;

namespace ShagOxServer.Application.DependencyInjections.LineItems;
public static class LineItemAttributeDependencyInjection
{
    public static IServiceCollection AddLineItemAttributeApplication(
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

        services.AddScoped<
            ICacheInvalidationService<LineItemAttribute, LineItemAttributeCacheContext>,
            LineItemAttributeInvalidationService>();

        services.AddScoped<LineItemAttributeInvalidationService>();

        services.AddScoped<LineItemAttributeValidator>();

        services.AddScoped<LineItemAttributeUpdateValidator>();


        return services;
    }
}
