using ShagOxServer.Application.Interfaces.Services.Payments;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.Payment;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Payments.PaymentProcessing;
public partial class PaymentProcessingService
    : IPaymentProcessingService
{
    public async Task<Result<bool>> ProcessSuccessfulPayment(
        string invoiceId,
        CancellationToken cancellationToken = default)
    {
        Order order;

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

            if(!webhooks.Any())
                return Result<bool>.Success(true);



            var paymentUpdated = await _paymentCommandRepository
                .TryMarkAsPaidAsync(invoiceId, cancellationToken);

            if (!paymentUpdated)
            {
                await _unitOfWork.RollbackAsync();
   
                return Result<bool>.Fail(
                    PaymentResources.PaymentStatusUpdateFailed);
            }



            order = payment.Order;

            var isPaid = await _orderCommandRepository
                .TryMarkAsPaidAsync(order.Id, cancellationToken);

            if (!isPaid)
            {
                await _unitOfWork.RollbackAsync();

                return Result<bool>.Success(true);
            }



            var deleteResult = await DeleteBasketItems(order);

            if (!deleteResult.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();

                return Result<bool>
                    .Fail(deleteResult.Error);
            }



            foreach (var webhook in webhooks)
            {
                await _webhookCommandRepository.UpdateStatusAsync(
                    webhook.Id,
                    webhook.Status,
                    cancellationToken);
            }

            await _unitOfWork.CommitAsync();

            return await _emailService
                .SendOrderPaidEmailAsync(order);
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
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
