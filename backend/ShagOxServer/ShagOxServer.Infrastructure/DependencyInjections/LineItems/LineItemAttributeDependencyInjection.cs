using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes.Query;
using ShagOxServer.Application.Interfaces.Repositories.LineItems.LineItemAttributes;
using ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.LineItemAttributes;
using ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.LineItemAttributes.Query;

namespace ShagOxServer.Infrastructure.DependencyInjections.LineItems;
public static class LineItemAttributeDependencyInjection
{
    public static IServiceCollection AddLineItemAttributeInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<ILineItemAttributeQueryRepository,
            LineItemAttributeQueryRepository>();

        services.AddScoped<ILineItemAttributeExistsRepository,
            LineItemAttributeExistsRepository>();

        return services;
    }
}
