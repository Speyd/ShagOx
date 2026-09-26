using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Mapping;
public static class AttributeDefinitionMapper
{
    public static AttributeDefinitionDto ToDto(
        AttributeDefinition attribute)
    {
        return new AttributeDefinitionDto(
            attribute.Id,
            attribute.CategoryId,
            attribute.Category.Code,
            attribute.Key,
            attribute.Type,
            attribute.Required,
            attribute.Min,
            attribute.Max,
            attribute.IsVariant,
            attribute.Multiple
        );
    }
}