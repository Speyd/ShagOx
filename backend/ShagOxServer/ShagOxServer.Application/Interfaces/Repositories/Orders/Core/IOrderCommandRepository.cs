using ShagOxServer.Domain.Entities.Orders.Enum;
using ShagOxServer.Domain.Entities.Payments.Enum;

namespace ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
public interface IOrderCommandRepository
{
    Task<bool> TryMarkAsPaidAsync(
        long orderId,
        CancellationToken cancellationToken = default);

    Task<bool> TryUpdatePaymentStatusAsync(
        long orderId,
        OrderPaymentStatus newStatus,
        CancellationToken cancellationToken = default);

    Task<bool> TryUpdateStatusAsync(
        long orderId,
        OrderStatus newStatus,
        CancellationToken cancellationToken = default);
}
