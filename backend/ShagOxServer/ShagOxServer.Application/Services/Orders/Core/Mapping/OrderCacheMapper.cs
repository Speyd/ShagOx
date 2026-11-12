using ShagOxServer.Application.DTOs.Orders.Core.Cache;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Services.Orders.Core.Mapping;
public static class OrderCacheMapper
{
    public static OrderCacheInfo ToInfo(
        Order order)
    {
        return new OrderCacheInfo(
            order.Id,
            order.OwnerId,
            order.OrderNumber,
            order.PaymentTransactionId,
            order.DeliveryTrackingNumber
        );
    }
}
