using Microsoft.Extensions.Logging;
using ShagOxServer.Application.Interfaces.Services.Payments;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.Payment;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Payment.PaymentProcessing;
public partial class PaymentProcessingService
    : IPaymentProcessingService
{
    public async Task<Result<bool>> ProcessSuccessfulPayment(
        string invoiceId,
        CancellationToken cancellationToken = default)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var payment = await _paymentRepository.GetByInvoiceIdAsync(
                invoiceId,
                cancellationToken);

            if (payment is null)
            {
                await _unitOfWork.RollbackAsync();

                return Result<bool>.NotFound(
                    EntityNamesResources.Payment);
            }

            var webhooks = await _webhookQueryRepository
                .GetByInvoiceIdAsync(
                    payment.InvoiceId,
                    cancellationToken,
                    PaymentStatus.Succeeded);

            if (!webhooks.Any())
            {
                await _unitOfWork.RollbackAsync();

                return Result<bool>.Success(true);
            }

            var paymentUpdated = await _paymentCommandRepository
                .TryMarkAsPaidAsync(invoiceId, cancellationToken);

            if (!paymentUpdated)
            {
                await _unitOfWork.RollbackAsync();
   
                return Result<bool>.Fail(
                    PaymentResources.PaymentStatusUpdateFailed);
            }

            var order = payment.Order;


            var isPaid = await _orderCommandRepository
                .TryMarkAsPaidAsync(
                    order.Id,
                    payment.InvoiceId,
                    cancellationToken);

            if (!isPaid)
            {
                // Гроші прийшли, а замовлення вже не чекає оплату
                // (скасоване покупцем/продавцем, прострочене або вже
                // оплачене іншим рахунком) — повертаємо кошти.
                await _unitOfWork.RollbackAsync();

                await RefundLatePaymentAsync(payment, cancellationToken);

                return Result<bool>.Success(true);
            }

            foreach (var webhook in webhooks)
            {
                await _webhookCommandRepository.UpdateStatusAsync(
                    webhook.Id,
                    webhook.Status,
                    cancellationToken);
            }

            await _unitOfWork.CommitAsync();

            await InvalidateOrderCacheAsync(order);

            // Прибирання кошика — поза транзакцією: якщо воно не вдасться,
            // оплата все одно має залишитися зарахованою.
            await TryDeleteBasketItemsAsync(order);

            return Result<bool>.Success(true);
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    private async Task RefundLatePaymentAsync(
        ShagOxServer.Domain.Entities.Payments.Payment payment,
        CancellationToken cancellationToken)
    {
        _logger.LogWarning(
            "Payment received for an order that no longer awaits payment. " +
            "Refunding. OrderId: {OrderId}, InvoiceId: {InvoiceId}",
            payment.OrderId,
            payment.InvoiceId);

        var refund = await _paymentGateway.RefundAsync(
            payment.InvoiceId,
            cancellationToken);

        if (!refund.IsSuccess)
        {
            _logger.LogError(
                "Automatic refund failed, manual refund required. " +
                "OrderId: {OrderId}, InvoiceId: {InvoiceId}",
                payment.OrderId,
                payment.InvoiceId);

            return;
        }

        await _paymentCommandRepository.TryUpdatePaymentStatusAsync(
            payment.InvoiceId,
            PaymentStatus.Refunded,
            cancellationToken);
    }

    private async Task TryDeleteBasketItemsAsync(Order order)
    {
        try
        {
            var result = await DeleteBasketItems(order);

            if (!result.IsSuccess)
            {
                _logger.LogWarning(
                    "Basket cleanup after payment skipped. OrderId: {OrderId}, Reason: {Reason}",
                    order.Id,
                    result.Error);
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(
                ex,
                "Basket cleanup after payment failed. OrderId: {OrderId}",
                order.Id);
        }
    }

    private async Task<Result<bool>> DeleteBasketItems(
        Order order,
        CancellationToken cancellationToken = default)
    {
        var basketId = await _basketRepository
            .GetIdByUserAsync(order.OwnerId);

        if (!basketId.HasValue)
        {
            return Result<bool>.Fail(
                EntityNamesResources.Basket);
        }

        var ids = order.OrderItems
            .Select(x =>
                (x.AdvertisementId, x.AdvertisementVariantId))
            .ToList();

        if (order.OrderItems is null || !order.OrderItems.Any())
        {
            return Result<bool>
                .Fail(EntityNamesResources.OrderItem);
        }


        var items = await _itemRepository
            .GetByParentAndDependenciesAsync(
                basketId.Value,
                ids,
                cancellationToken);

        foreach (var item in items)
        {
            var deleteResult = await _itemDeleteService
                .SoftDeleteRecordAsync(item, cancellationToken);

            if (!deleteResult.IsSuccess)
            {
                return Result<bool>
                    .Fail(deleteResult.Error);
            }
        }

        return Result<bool>.Success(true);
    }
}
