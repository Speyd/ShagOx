using ShagOxServer.Application.Interfaces.Services.Payments;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.Payment;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Orders.Enum;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Payment.PaymentProcessing;
public partial class PaymentProcessingService
    : IPaymentProcessingService
{
    public async Task<Result<bool>> ProcessFailedPayment(
        string invoiceId,
        CancellationToken cancellationToken = default)
    {
        var payment = await _paymentRepository.GetByInvoiceIdAsync(
            invoiceId,
            cancellationToken);

        if (payment is null)
        {
            return Result<bool>.NotFound(
                EntityNamesResources.Payment);
        }

        return await ProcessFailedPayment(payment, cancellationToken);
    }
    public async Task<Result<bool>> ProcessFailedPayment(
        ShagOxServer.Domain.Entities.Payments.Payment payment,
        CancellationToken cancellationToken = default)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var webhooks = await _webhookQueryRepository
                .GetByInvoiceIdAsync(
                    payment.InvoiceId,
                    cancellationToken,
                    PaymentStatus.Failed,
                    PaymentStatus.Cancelled);

            if (payment.Status == PaymentStatus.Succeeded &&
                !webhooks.Any())
            {
                await _unitOfWork.RollbackAsync();

                return Result<bool>.Fail(
                    PaymentResources.PaymentAlreadySucceeded);
            }

            // Платіж уже оброблено (оплачено, відхилено або прострочено
            // раніше) — повторна обробка не повинна вдруге скасовувати
            // замовлення чи повертати товари на склад.
            var paymentFailed = await _paymentCommandRepository
                .TryMarkAsFailedAsync(
                    payment.InvoiceId,
                    cancellationToken);

            if (!paymentFailed)
            {
                await _unitOfWork.RollbackAsync();

                return Result<bool>.Success(true);
            }

            var updateResult = await UpdateOrderStatusAsync(
                payment,
                cancellationToken);

            if (!updateResult.IsSuccess)
                return updateResult;

            await RestoreOrderStockAsync(
                payment.Order,
                cancellationToken);

            foreach (var webhook in webhooks)
            {
                await _webhookCommandRepository.UpdateStatusAsync(
                    webhook.Id,
                    webhook.Status,
                    cancellationToken);
            }

            await _unitOfWork.CommitAsync();

            await InvalidateOrderCacheAsync(payment.Order);

            return Result<bool>.Success(true);
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    private async Task<Result<bool>> UpdateOrderStatusAsync(
        ShagOxServer.Domain.Entities.Payments.Payment payment,
        CancellationToken cancellationToken)
    {
        var orderId = payment.OrderId;

        var orderPayment = await _orderCommandRepository
            .TryUpdatePaymentStatusAsync(
                orderId,
                OrderPaymentStatus.Failed,
                cancellationToken);

        if (!orderPayment)
        {
            await _unitOfWork.RollbackAsync();

            return Result<bool>.NotFound(
                EntityNamesResources.Order);
        }

        var orderStatus = await _orderCommandRepository
            .TryUpdateStatusAsync(
                orderId,
                OrderStatus.Cancelled,
                cancellationToken);

        if (!orderStatus)
        {
            await _unitOfWork.RollbackAsync();

            return Result<bool>.NotFound(
                EntityNamesResources.Order);
        }


        var cancelledOrder = await _orderCommandRepository
            .TryMarkAsCancelledAsync(
                orderId,
                cancellationToken);

        if (!cancelledOrder)
        {
            await _unitOfWork.RollbackAsync();

            return Result<bool>.NotFound(
                EntityNamesResources.Order);
        }

        return Result<bool>.Success(true);
    }

    private async Task RestoreOrderStockAsync(
        Order order,
        CancellationToken cancellationToken)
    {
        foreach (var item in order.OrderItems)
        {
            if (item.AdvertisementVariantId.HasValue)
            {
                await _variantCommand.IncreaseStockAsync(
                    item.AdvertisementVariantId.Value,
                    item.Quantity);
            }
            else
            {
                await _advertCommand.IncreaseStockAsync(
                    item.AdvertisementId,
                    item.Quantity);
            }
        }
    }
}
