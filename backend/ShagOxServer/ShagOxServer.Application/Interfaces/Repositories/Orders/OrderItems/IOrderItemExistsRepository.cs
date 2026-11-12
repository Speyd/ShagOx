using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Special;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Interfaces.Repositories.Orders.OrderItems;
public interface IOrderItemExistsRepository
    : IExistsRepository<OrderItem>, IExistsOwnerRepository
{
    Task<bool> ExistsAsync(
        long orderId,
        long itemId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
