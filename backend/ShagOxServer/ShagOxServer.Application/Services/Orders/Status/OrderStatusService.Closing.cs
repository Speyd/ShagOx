using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Orders;
using ShagOxServer.Application.Resources.Orders.Order;
using ShagOxServer.Application.Resources.Payment;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Orders.Enum;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Enum;

namespace ShagOxServer.Application.Services.Orders.Status;
public partial class OrderStatusService
{
    private async Task<Result<OrderStatusResponse>> CloseAsync(
        Order order,
        IReadOnlyCollection<OrderStatus> allowedFrom,
        OrderStatus newStatus,
        CancellationToken cancellationToken)
    {
        var expectedPaymentStatus = order.PaymentStatus;
        var isPaidOnline =
            order.PaymentMethod == PaymentMethod.Online &&
            order.PaymentStatus == OrderPaymentStatus.Paid;

        var paidPayment = isPaidOnline
            ? await _paymentQuery.GetSucceededByOrderIdAsync(order.Id, cancellationToken)
            : null;

        if (isPaidOnline && paidPayment is null)
        {
            _logger.LogError(
                "Paid order has no succeeded payment to refund. OrderId: {OrderId}",
                order.Id);

            return Fail(PaymentResources.PaymentRefundFailed, ResultErrorType.InternalServer);
        }

        var openPayments = order.PaymentMethod == PaymentMethod.Online && !isPaidOnline
            ? await _paymentQuery.GetOpenByOrderIdAsync(order.Id, cancellationToken)
            : [];

        var changedAt = DateTime.UtcNow;

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            foreach (var payment in openPayments)
            {
                await _paymentCommand.TryMarkAsFailedAsync(
                    payment.InvoiceId,
                    cancellationToken);
            }

            var closed = await _orderCommands.TryCloseAsync(
                order.Id,
                allowedFrom,
                newStatus,
                expectedPaymentStatus,
                cancellationToken);

            if (!closed)
            {
                await _unitOfWork.RollbackAsync();

                return Fail(OrderResources.OrderStatusChanged, ResultErrorType.Conflict);
            }

            await ReturnStockAsync(order, cancellationToken);

            if (paidPayment is not null)
            {
                var refund = await _paymentGateway.RefundAsync(
                    paidPayment.InvoiceId,
                    cancellationToken);

                if (!refund.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();

                    return Fail(refund.Error!, ResultErrorType.BadRequest);
                }

                await _paymentCommand.TryUpdatePaymentStatusAsync(
                    paidPayment.InvoiceId,
                    PaymentStatus.Refunded,
                    cancellationToken);

                await _orderCommands.TryUpdatePaymentStatusAsync(
                    order.Id,
                    OrderPaymentStatus.Refunded,
                    cancellationToken);
            }

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to close order. OrderId: {OrderId}, NewStatus: {Status}",
                order.Id,
                newStatus);

            return Fail(OrderResources.OrderStatusUpdateFailed, ResultErrorType.InternalServer);
        }

        foreach (var payment in openPayments)
        {
            await _paymentGateway.CancelInvoiceAsync(payment.InvoiceId, cancellationToken);
        }

        _logger.LogInformation(
            "Order closed. OrderId: {OrderId}, {From} -> {To}, Refunded: {Refunded}",
            order.Id,
            order.OrderStatus,
            newStatus,
            paidPayment is not null);

        return Result<OrderStatusResponse>.Success(
            new OrderStatusResponse(order.Id, newStatus, changedAt));
    }

    private async Task<Result<OrderStatusResponse>> FinishAsync(
        Order order,
        bool changed,
        OrderStatus newStatus,
        bool stockChanged)
    {
        if (!changed)
            return Fail(OrderResources.OrderStatusChanged, ResultErrorType.Conflict);

        _logger.LogInformation(
            "Order status changed. OrderId: {OrderId}, {From} -> {To}",
            order.Id,
            order.OrderStatus,
            newStatus);

        return Result<OrderStatusResponse>.Success(
            new OrderStatusResponse(order.Id, newStatus, DateTime.UtcNow));
    }
}
