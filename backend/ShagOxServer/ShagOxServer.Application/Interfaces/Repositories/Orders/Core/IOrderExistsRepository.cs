using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
public interface IOrderExistsRepository
    : IExistsRepository<Order>
{
    Task<bool> ExistsByOrderNumberAsync(
        string orderNumber,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<bool> ExistsByPaymentTransactionIdAsync(
        string paymentTransactionId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<bool> ExistsByDeliveryTrackingNumberAsync(
        string trackingNumber,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
