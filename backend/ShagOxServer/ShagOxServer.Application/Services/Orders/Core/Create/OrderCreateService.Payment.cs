using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Orders.Core.Create;
using ShagOxServer.Application.DTOs.Payment;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Orders.Enum;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Orders.Core.Create;
public partial class OrderCreateService
    : IOrderCreateService
{
    private async Task<Result<OrderCreateResponse>> InitiateOrderPaymentAsync(
        Order order,
        CancellationToken cancellationToken)
    {
        if (order.PaymentMethod != PaymentMethod.Online)
        {
            var emailResult =  await _emailService
                .SendOrderPaidAsync(order);

            if (!emailResult.IsSuccess)
            {
                await _orderCommands.TryMarkAsFailedAsync(order.Id);

                
                return Result<OrderCreateResponse>
                    .Fail(emailResult.Error);
            }

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
                await _orderCommands.TryMarkAsFailedAsync(order.Id);

                return Result<OrderCreateResponse>
                    .Fail(paymentResult.Error);
            }

            return Result<OrderCreateResponse>.Success(
                new OrderCreateResponse(
                    order.Id,
                    paymentResult.Value,
                    DateTime.UtcNow));
        }
        catch (Exception ex)
        {
            await _orderCommands.TryMarkAsFailedAsync(order.Id);

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

        await _unitOfWork.SaveChangesAsync();

        return Result<string>.Success(result.Value.PaymentUrl);
    }
}
