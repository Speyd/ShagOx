using ShagOxServer.Application.DTOs.Orders;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.Orders.Order;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Enum;

namespace ShagOxServer.Application.Services.Orders.Status;
public partial class OrderStatusService
{
    private async Task<(Order? Order, Result<OrderStatusResponse>? Error)> GetSellerOrderAsync(
        long orderId,
        long sellerId,
        CancellationToken cancellationToken)
    {
        var order = await _orderCommand
            .GetForPaymentAsync(orderId, cancellationToken);

        if (order is null)
            return (null, Result<OrderStatusResponse>.NotFound(EntityNamesResources.Order));

        return (order, CheckSellerAccess(order, sellerId));
    }

    private static Result<OrderStatusResponse>? CheckSellerAccess(
        Order order,
        long sellerId)
    {
        var items = order.OrderItems;

        if (!items.Any(i => i.Advertisement.OwnerId == sellerId))
            return Fail(OrderResources.OrderNotSellerOrder, ResultErrorType.Forbidden);

        if (items.Any(i => i.Advertisement.OwnerId != sellerId))
            return Fail(OrderResources.OrderHasOtherSellersItems, ResultErrorType.Forbidden);

        return null;
    }
}
