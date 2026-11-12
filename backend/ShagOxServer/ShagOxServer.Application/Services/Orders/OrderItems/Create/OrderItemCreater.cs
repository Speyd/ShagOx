using ShagOxServer.Application.DTOs.LineItems.Create;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Services.Orders.OrderItems.Create;
public static class OrderItemCreater
{
    public static OrderItem Create(
        LineItemCreateRequest request,
        decimal price)
    {
        return new OrderItem
        {
            ParentId = request.ParentId,
            AdvertisementId = request.AdvertisementId,
            AdvertisementVariantId = request.AdvertisementVariantId,
            Quantity = request.Quantity,
            TotalPrice = request.Quantity * price,
        };
    }
}
