using ShagOxServer.Application.DTOs.Advertisements.Core.Query.LineItems;
using ShagOxServer.Application.DTOs.Orders.OrderItems.Query;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Services.Orders.OrderItems.Mapping;
public static class OrderItemMapper
{
    public static OrderItemDto ToDto(
        OrderItem item,
        AdvertisementLineItemDto advert)
    {
        return new OrderItemDto(
            item.Id,
            item.EntityStatus,
            item.ParentId,
            item.AdvertisementId,
            item.AdvertisementVariantId,
            item.Quantity,
            item.Price,
            item.TotalPrice,
            advert
        );
    }
}
