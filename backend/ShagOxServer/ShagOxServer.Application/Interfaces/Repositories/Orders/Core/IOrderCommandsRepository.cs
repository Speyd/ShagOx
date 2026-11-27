using ShagOxServer.Domain.Entities.Orders.Enum;
using ShagOxServer.Domain.Entities.Payments.Enum;

namespace ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
public interface IOrderCommandsRepository
{
    Task<bool> TryMarkAsFailedAsync(
        long orderId,
        CancellationToken cancellationToken = default);

    Task<bool> TryMarkAsPaidAsync(
        long orderId,
        string? transactionId = null,
        CancellationToken cancellationToken = default);

    Task<bool> TryUpdatePaymentStatusAsync(
        long orderId,
        OrderPaymentStatus newStatus,
        CancellationToken cancellationToken = default);

    Task<bool> TryMarkAsCancelledAsync(
        long orderId,
        CancellationToken cancellationToken = default);

    Task<bool> TryUpdateStatusAsync(
        long orderId,
        OrderStatus newStatus,
        CancellationToken cancellationToken = default);

    Task<bool> TryConfirmAsync(
        long orderId,
        CancellationToken cancellationToken = default);

    Task<bool> TryCloseAsync(
        long orderId,
        IReadOnlyCollection<OrderStatus> allowedFrom,
        OrderStatus newStatus,
        OrderPaymentStatus expectedPaymentStatus,
        CancellationToken cancellationToken = default);

    Task<bool> TryShipAsync(
        long orderId,
        OrderStatus newStatus,
        string? trackingNumber,
        string? trackingUrl,
        CancellationToken cancellationToken = default);

    Task<bool> TryCompleteAsync(
        long orderId,
        IReadOnlyCollection<OrderStatus> allowedFrom,
        CancellationToken cancellationToken = default);

    Task<bool> TryReopenForPaymentAsync(
        long orderId,
        CancellationToken cancellationToken = default);
}
