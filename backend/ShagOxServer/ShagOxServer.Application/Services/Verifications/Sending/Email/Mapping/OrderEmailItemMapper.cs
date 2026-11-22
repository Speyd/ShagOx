using ShagOxServer.Application.DTOs.Orders.Core.Query.Email;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Services.Verifications.Sending.Email.Mapping;
public static class OrderEmailItemMapper
{
    public static OrderEmailItem ToDto(
        OrderItem item)
    {
        return new OrderEmailItem(
            item.AdvertisementVariant?.Advertisement.Title
                ?? item.Advertisement.Title,
            item.AdvertisementVariant?.Advertisement.Pictures.FirstOrDefault()?.Url
                ?? item.Advertisement.Pictures.FirstOrDefault()?.Url,
            item.Price,
           item.Quantity);
    }
}
