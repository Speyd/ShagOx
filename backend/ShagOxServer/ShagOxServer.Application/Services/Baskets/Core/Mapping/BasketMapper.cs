using ShagOxServer.Application.DTOs.Baskets.Core;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.Core.Mapping;
public static class BasketMapper
{
    public static BasketDto ToDto(
        Basket basket)
    {
        return new BasketDto(
            basket.Id,
            basket.UserId
        );
    }
}