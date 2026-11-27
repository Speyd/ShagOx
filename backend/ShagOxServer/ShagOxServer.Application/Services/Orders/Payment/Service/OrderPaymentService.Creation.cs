using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Orders;
using ShagOxServer.Application.DTOs.Payment;
using ShagOxServer.Application.Resources.Payment;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Orders.Payment;
public partial class OrderPaymentService
{
    private async Task<Result<OrderPaymentResponse>> CreatePaymentAsync(
        Order order,
        bool reopen,
        CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            if (reopen)
            {
                var reopened = await _orderCommandRepository
                    .TryReopenForPaymentAsync(order.Id, cancellationToken);

                if (!reopened)
                {
                    await _unitOfWork.RollbackAsync();

                    return Result<OrderPaymentResponse>.Fail(
                        PaymentResources.OrderPaymentUnavailable);
                }

                var stockResult = await ReserveStockAsync(
                    order,
                    cancellationToken);

                if (!stockResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();

                    return Result<OrderPaymentResponse>
                        .Fail(stockResult.Error);
                }
            }

            var paymentResult = await _paymentProvider.PaymentRequest(
                new PaymentRequest(
                    order.TotalPrice,
                    order.Id,
                    order.Currency.NumericCode,
                    order.Currency.DecimalPlaces),
                cancellationToken);

            if (!paymentResult.IsSuccess || paymentResult.Value is null)
            {
                await _unitOfWork.RollbackAsync();

                return Result<OrderPaymentResponse>
                    .Fail(paymentResult.Error);
            }

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create payment. OrderId: {OrderId}",
                order.Id);

            return Result<OrderPaymentResponse>.Fail(
                PaymentResources.PaymentProviderInvalidResponse);
        }

        var created = await _paymentQueryRepository
            .GetActiveByOrderIdAsync(
                order.Id,
                DateTime.UtcNow,
                cancellationToken);

        if (created?.PaymentUrl is null)
        {
            return Result<OrderPaymentResponse>.Fail(
                PaymentResources.PaymentProviderInvalidResponse);
        }

        _logger.LogInformation(
            "Payment link issued. OrderId: {OrderId}, Reopened: {Reopened}",
            order.Id,
            reopen);

        return Result<OrderPaymentResponse>.Success(
            new OrderPaymentResponse(
                created.PaymentUrl,
                created.ExpiresAt));
    }
}
