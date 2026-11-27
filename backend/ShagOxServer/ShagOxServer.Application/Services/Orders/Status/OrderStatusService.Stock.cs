using ShagOxServer.Application.DTOs.Orders;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Enum;

namespace ShagOxServer.Application.Services.Orders.Status;
public partial class OrderStatusService
{
    private async Task ReturnStockAsync(
        Order order,
        CancellationToken cancellationToken)
    {
        foreach (var item in order.OrderItems)
        {
            if (item.AdvertisementVariantId is long variantId)
            {
                await _variantCommand.IncreaseStockAsync(
                    variantId, item.Quantity, cancellationToken);
            }
            else
            {
                await _advertCommand.IncreaseStockAsync(
                    item.AdvertisementId, item.Quantity, cancellationToken);
            }
        }
    }

    private static Result<OrderStatusResponse> Fail(
        string error,
        ResultErrorType errorType = ResultErrorType.BadRequest)
    {
        return Result<OrderStatusResponse>.Fail(error, errorType);
    }
}
