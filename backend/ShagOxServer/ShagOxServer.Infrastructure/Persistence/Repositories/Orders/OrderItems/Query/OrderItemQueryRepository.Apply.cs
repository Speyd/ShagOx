using ShagOxServer.Application.Interfaces.Repositories.Orders.OrderItems;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Filters.Orders.OrderItems;
using ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.Core.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Orders.OrderItems.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Orders.OrderItems.Query;
public partial class OrderItemQueryRepository
    : LineItemQueryRepository<OrderItem, OrderItemSearchFilter>,
      IOrderItemQueryRepository
{
    protected override IQueryable<OrderItem> ApplyIncludes(
        IQueryable<OrderItem> query)
    {
        return query.WithIncludes();
    }

    public override IQueryable<OrderItem> ApplyFilter(
      IQueryable<OrderItem> query,
      OrderItemSearchFilter filter)
    {
        return query.Filter(filter);
    }
}
