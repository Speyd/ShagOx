using ShagOxServer.Application.DTOs.Baskets.Core.Cache;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.Core.Mapping;
public static class BasketCacheMapper
{
    public static BasketCacheInfo ToInfo(
        Basket item)
    {
        return new BasketCacheInfo(
            item.Id,
            item.UserId
        );
    }
}