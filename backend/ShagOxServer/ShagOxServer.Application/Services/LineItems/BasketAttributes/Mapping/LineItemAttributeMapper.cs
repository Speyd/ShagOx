using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.DTOs.LineItems.LineItemAttributes.Query;
using ShagOxServer.Domain.Entities.LineItems;

namespace ShagOxServer.Application.Services.LineItems.LineItemAttributes.Mapping;
public static class LineItemAttributeMapper
{
    public static LineItemAttributeDto ToDto(
        LineItemAttribute attribute,
        AttributeDefinitionDto attributeDef)
    {
        return new LineItemAttributeDto(
            attribute.Id,
            attribute.EntityStatus,
            attributeDef,
            attribute.Order
        );
    }
}
