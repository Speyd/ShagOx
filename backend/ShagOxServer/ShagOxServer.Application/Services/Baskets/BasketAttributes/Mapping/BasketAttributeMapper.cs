using ShagOxServer.Application.DTOs.Baskets;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Mapping;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.BasketAttributes.Mapping;
public static class BasketAttributeMapper
{
    public static BasketAttributeDto ToDto(
        BasketAttribute attribute)
    {
        return new BasketAttributeDto(
            attribute.Id,
            AttributeDefinitionMapper
                .ToDto(attribute.AttributeDefinition),
            attribute.Order
        );
    }
}