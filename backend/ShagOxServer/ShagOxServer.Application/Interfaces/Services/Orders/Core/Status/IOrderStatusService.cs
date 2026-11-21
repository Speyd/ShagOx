using ShagOxServer.Application.DTOs.Orders.Core.Status;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Orders.Core.Status;

/// <summary>
/// Дії із замовленням на сайті.
/// Покупець: скасувати (до відправки), підтвердити отримання.
/// Продавець: підтвердити, відхилити, відправити (ТТН) / підготувати до видачі,
/// завершити.
/// </summary>
public interface IOrderStatusService
{
    Task<Result<OrderStatusResponse>> CancelByBuyerAsync(
        long orderId,
        long buyerId,
        CancellationToken cancellationToken = default);

    Task<Result<OrderStatusResponse>> ConfirmBySellerAsync(
        long orderId,
        long sellerId,
        CancellationToken cancellationToken = default);

    Task<Result<OrderStatusResponse>> RejectBySellerAsync(
        long orderId,
        long sellerId,
        CancellationToken cancellationToken = default);

    Task<Result<OrderStatusResponse>> ShipBySellerAsync(
        long orderId,
        long sellerId,
        string? trackingNumber,
        CancellationToken cancellationToken = default);

    /// <summary>Отримання підтверджує покупець або продавець.</summary>
    Task<Result<OrderStatusResponse>> CompleteAsync(
        long orderId,
        long userId,
        CancellationToken cancellationToken = default);
}
