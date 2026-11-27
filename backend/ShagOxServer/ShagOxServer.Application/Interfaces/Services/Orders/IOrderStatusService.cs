using ShagOxServer.Application.DTOs.Orders;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Orders;
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

    Task<Result<OrderStatusResponse>> CompleteAsync(
        long orderId,
        long userId,
        CancellationToken cancellationToken = default);
}
