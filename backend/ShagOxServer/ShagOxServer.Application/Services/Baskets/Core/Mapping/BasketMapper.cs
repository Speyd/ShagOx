using ShagOxServer.Application.DTOs.Baskets.BasketItems.Query;
using ShagOxServer.Application.DTOs.Baskets.Core.Query;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.Core.Mapping;
public static class BasketMapper
{
    public static BasketDto ToDto(
        Basket basket,
        List<BasketItemDto> items)
    {
        return new BasketDto(
            basket.Id,
            basket.UserId,
            items
        );
    }
}