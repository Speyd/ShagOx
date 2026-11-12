using ShagOxServer.Application.Interfaces.Repositories.Base.Items.Query;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Filters.Orders.OrderItems;

namespace ShagOxServer.Application.Interfaces.Repositories.Orders.OrderItems;
public interface IOrderItemQueryRepository
    : ILineItemQueryRepository<OrderItem, OrderItemSearchFilter>
{
}
