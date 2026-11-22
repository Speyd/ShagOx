using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Orders.Core.Create;
using ShagOxServer.Application.DTOs.Payment;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Orders.Core.Mapping;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Orders.Core.Create;
public partial class OrderCreateService
    : IOrderCreateService
{
    private async Task<Result<OrderCreateResponse>> ExecuteWithErrorHandlingAsync(
        Order order,
        CancellationToken cancellationToken)
    {
        if (order.PaymentMethod != PaymentMethod.Online)
        {
            return Result<OrderCreateResponse>.Success(
                new OrderCreateResponse(
                    order.Id,
                    null,
                    DateTime.UtcNow));
        }

        try
        {
            var paymentResult = await MakePayment(
                order,
                cancellationToken);

            if (!paymentResult.IsSuccess)
            {
                return Result<OrderCreateResponse>
                    .Fail(paymentResult.Error);
            }

            await _orderInvalidation.InvalidateCreateAsync(
                OrderCacheMapper.ToInfo(order));

            return Result<OrderCreateResponse>.Success(
                new OrderCreateResponse(
                    order.Id,
                    paymentResult.Value,
                    DateTime.UtcNow));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Payment failed. OrderId: {OrderId}",
                order.Id);

            return Result<OrderCreateResponse>
                .Fail(EntityErrorResources.OrderCreateFailed);
        }
    }

    private async Task<Result<string>> MakePayment(
        Order order,
        CancellationToken cancellationToken)
    {
        var request = new PaymentRequest
        (
            order.TotalPrice,
            order.Id,
            order.Currency.NumericCode,
            order.Currency.DecimalPlaces
        );

        var result = await _paymentProvider
            .PaymentRequest(request, cancellationToken);

        if (!result.IsSuccess || result.Value is null)
            return Result<string>.Fail(result.Error);

        order.PaymentStatus = OrderPaymentStatus.Pending;

        _orderRepository.Update(order);

        await _unitOfWork.SaveChangesAsync();

        await _webhookProcessor.ProcessPendingAsync(
            result.Value.InvoiceId,
            cancellationToken);

        return Result<string>.Success(result.Value.PaymentUrl);
    }
}
