using ShagOxServer.Application.DTOs.LineItems.Core.Cache;
using ShagOxServer.Application.DTOs.Orders.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Orders.OrderItems;
public class OrderItemOrderInvalidationService
    : ICacheInvalidationService<OrderItem, LineItemCacheInfo>
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
       LineItemCacheInfo entityInfo)
    {
        var orderInfo = await GetOrderInfos(
            entityInfo.ParentId);

        if (orderInfo is null)
            return;

        await _orderInvalid.InvalidateUpdateAsync(orderInfo);
    }


    public async Task InvalidateDeleteAsync(
        LineItemCacheInfo entityInfo)
    {
        var orderInfo = await GetOrderInfos(
             entityInfo.ParentId);

        if (orderInfo is null)
            return;

        await _orderInvalid.InvalidateDeleteAsync(orderInfo);
    }

    public async Task InvalidateUpdateAsync(
        LineItemCacheInfo entityInfo)
    {
        var orderInfo = await GetOrderInfos(
             entityInfo.ParentId);

        if (orderInfo is null)
            return;

        await _orderInvalid.InvalidateUpdateAsync(orderInfo);
    }

    private async Task<OrderCacheInfo?> GetOrderInfos(
        long orderId)
    {
        var order = await _orderRepository
           .GetByIdAsync(orderId);

        if (order is null)
            return null;

        return new OrderCacheInfo(
            order.Id,
            order.OwnerId,
            order.OrderNumber,
            order.PaymentTransactionId,
            order.DeliveryTrackingNumber,
            order.EntityStatus);
    }
}
