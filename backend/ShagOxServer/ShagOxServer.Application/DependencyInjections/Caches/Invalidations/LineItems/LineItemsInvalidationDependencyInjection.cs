using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Caches.Invalidations.LineItems.Localized;
using ShagOxServer.Application.Services.Caches.Invalidations.LineItems;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.LineItems;
public static class LineItemsInvalidationDependencyInjection
{
    public static IServiceCollection AddLineItemsInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<LineItemAttributeInvalidationService>();

        services.AddLineItemsLocalizedInvalidationApplication();


        return services;
    }
}
