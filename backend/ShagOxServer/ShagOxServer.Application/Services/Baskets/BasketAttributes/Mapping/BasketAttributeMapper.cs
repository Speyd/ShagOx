using ShagOxServer.Application.DTOs.Baskets.BasketAttributes;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.BasketAttributes.Mapping;
public static class BasketAttributeMapper
{
    public static BasketAttributeDto ToDto(
        BasketAttribute attribute,
        AttributeDefinitionDto attributeDef)
    {
        return new BasketAttributeDto(
            attribute.Id,
            attributeDef,
            attribute.Order
        );
    }
}