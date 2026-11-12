using ShagOxServer.Application.Interfaces.Repositories.LineItems.Core;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Interfaces.Repositories.Orders.OrderItems;
public interface IOrderItemExistsRepository
    : ILineItemExistsRepository<OrderItem>
{
}
