using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Filters.Orders.Core;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Orders.Core.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Orders.Core.Query;
public partial class OrderQueryRepository
    : SearchRepository<Order, OrderSearchFilter>,
      IOrderQueryRepository
{
    protected override IQueryable<Order> ApplyIncludes(
        IQueryable<Order> query)
    {
        return query.WithIncludes();
    }

    public override IQueryable<Order> ApplyFilter(
        IQueryable<Order> query,
        OrderSearchFilter filter)
    {
        return query.Filter(filter);
    }
}
