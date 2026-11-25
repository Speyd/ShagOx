using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Application.DependencyInjections.LineItems;
public static class LineItemsDependencyInjection
{
    public static IServiceCollection AddLineItemsApplication(
        this IServiceCollection services)
    {
        services.AddLineItemAttributeApplication();


        return services;
    }
}
