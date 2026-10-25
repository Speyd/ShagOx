using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Cache;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.BasketAttributes.Mapping;
public static class BasketAttributeCacheMapper
{
    public static BasketAttributeCacheInfo ToInfo(
        BasketAttribute attribute,
        long categoryId)
    {
        return new BasketAttributeCacheInfo(
            attribute.Id,
            categoryId,
            attribute.AttributeDefinitionId
        );
    }
}
