using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Infrastructure.DependencyInjections.LineItems;
public static class LineItemsDependencyInjection
{
    public static IServiceCollection AddLineItemsInfrastructure(
        this IServiceCollection services)
    {
        services.AddLineItemAttributeInfrastructure();

        return services;
    }
}
