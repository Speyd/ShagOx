using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Services.Caches.Invalidations.LineItems.Localized;

namespace ShagOxServer.Application.DependencyInjections.Caches.Invalidations.LineItems.Localized;
public static class LineItemsLocalizedInvalidationDependencyInjection
{
    public static IServiceCollection AddLineItemsLocalizedInvalidationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<LineItemAttributeLocalizedInvalidationService>();


        return services;
    }
}
