using ShagOxServer.Application.DTOs.Baskets.Core;
using ShagOxServer.Application.Services.Baskets.BasketItems.Mapping;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.Core.Mapping;
public static class BasketMapper
{
    public static BasketDto ToDto(
        Basket basket)
    {
        return new BasketDto(
            basket.Id,
            basket.UserId,
            basket.BasketItems
                .Select(BasketItemMapper.ToDto)
                .ToList()
        );
    }
}