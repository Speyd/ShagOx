using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Domain.Cache.LineItems;
using ShagOxServer.Domain.Cache.Orders;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Orders.OrderItems;
public class OrderItemOrderInvalidationService
    : ICacheInvalidationService<OrderItem, LineItemCacheContext>
{
    private readonly IOrderQueryRepository _orderRepository;
    private readonly OrderInvalidationService _orderInvalid;


    public OrderItemOrderInvalidationService(
        IOrderQueryRepository orderRepository,
        OrderInvalidationService orderInvalid)
    {
        _orderRepository = orderRepository;
        _orderInvalid = orderInvalid;
    }


    public async Task InvalidateCreateAsync(
       LineItemCacheContext entityInfo)
    {
        var orderInfo = await GetOrderInfos(
            entityInfo.ParentId);

        if (orderInfo is null)
            return;

        await _orderInvalid.InvalidateUpdateAsync(orderInfo);
    }


    public async Task InvalidateDeleteAsync(
        LineItemCacheContext entityInfo)
    {
        var orderInfo = await GetOrderInfos(
             entityInfo.ParentId);

        if (orderInfo is null)
            return;

        await _orderInvalid.InvalidateDeleteAsync(orderInfo);
    }

    public async Task InvalidateUpdateAsync(
        LineItemCacheContext entityInfo)
    {
        var orderInfo = await GetOrderInfos(
             entityInfo.ParentId);

        if (orderInfo is null)
            return;

        await _orderInvalid.InvalidateUpdateAsync(orderInfo);
    }

    private async Task<OrderCacheContext?> GetOrderInfos(
        long orderId)
    {
        var order = await _orderRepository
           .GetByIdAsync(orderId);

        if (order is null)
            return null;

        return new OrderCacheContext(
            order.Id,
            order.OwnerId,
            order.OrderNumber,
            order.PaymentTransactionId,
            order.DeliveryTrackingNumber,
            order.EntityStatus);
    }
}
