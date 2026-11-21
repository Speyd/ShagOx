using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Orders.Enum;
using ShagOxServer.Domain.Entities.Payments.Enum;

namespace ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
public interface IOrderCommandRepository
{
    /// <summary>
    /// Позначає оплаченим лише замовлення, що ще чекає оплату
    /// (OrderStatus = Pending, PaymentStatus = Pending).
    /// </summary>
    Task<bool> TryMarkAsPaidAsync(
        long orderId,
        string? transactionId = null,
        CancellationToken cancellationToken = default);

    /// <summary>Pending → Confirmed (онлайн-замовлення — лише оплачене).</summary>
    Task<bool> TryConfirmAsync(
        long orderId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Скасування або відхилення, якщо поточний статус входить у allowedFrom,
    /// а статус оплати не змінився з моменту читання (expectedPaymentStatus) —
    /// інакше оплачене тим часом замовлення закрилося б без повернення коштів.
    /// </summary>
    Task<bool> TryCloseAsync(
        long orderId,
        IReadOnlyCollection<OrderStatus> allowedFrom,
        OrderStatus newStatus,
        OrderPaymentStatus expectedPaymentStatus,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Confirmed → Shipped (доставка) або ReadyForShipment (самовивіз).
    /// </summary>
    Task<bool> TryShipAsync(
        long orderId,
        OrderStatus newStatus,
        string? trackingNumber,
        string? trackingUrl,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// → Completed. Для оплати при отриманні також позначає оплату.
    /// </summary>
    Task<bool> TryCompleteAsync(
        long orderId,
        IReadOnlyCollection<OrderStatus> allowedFrom,
        CancellationToken cancellationToken = default);

    /// <summary>Різні власники товарів замовлення (читає з primary).</summary>
    Task<List<long>> GetSellerIdsAsync(
        long orderId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Повертає скасоване через неуспішну оплату онлайн-замовлення
    /// в стан очікування оплати (Pending/Pending).
    /// </summary>
    Task<bool> TryReopenForPaymentAsync(
        long orderId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Замовлення з валютою, службою доставки та активними товарами
    /// (оголошення, їхні статуси й варіанти). Читає з primary.
    /// </summary>
    Task<Order?> GetForPaymentAsync(
        long orderId,
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
}
