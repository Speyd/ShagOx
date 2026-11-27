using ShagOxServer.Application.DTOs.Orders.Core.Query.Email.Create;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Services.Verifications.Sending.Email.Mapping.Order.Create;
public static class OrderEmailItemMapper
{
    public static OrderEmailItem ToDto(
        OrderItem item)
    {
        var advert = item.Advertisement;

        return new OrderEmailItem(
            advert.Title,
            advert.Pictures.FirstOrDefault()?.Url,
            item.Price,
            item.Quantity);
    }
}
