using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Orders.Core.Extensions;
public static class OrderQueryExtensions
{
    public static IQueryable<Order> WithIncludes(
        this IQueryable<Order> query)
    {
        return query;
    }
}
