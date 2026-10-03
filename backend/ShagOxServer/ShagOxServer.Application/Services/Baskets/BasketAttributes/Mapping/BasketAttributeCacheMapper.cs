using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Cache;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.BasketAttributes.Mapping;
public static class BasketAttributeCacheMapper
{
    public static BasketAttributeCacheInfo ToInfo(
        BasketAttribute attribute)
    {
        return new BasketAttributeCacheInfo(
            attribute.Id,
            attribute.AttributeDefinition.CategoryId,
            attribute.AttributeDefinitionId
        );
    }
}