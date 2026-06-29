using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Mapping;
public static class AttributeDefinitionMapper
{
    public static AttributeDefinitionDto ToDto(
        AttributeDefinition attribute)
    {
        return new AttributeDefinitionDto(
            attribute.Id,
            attribute.CategoryId,
            attribute.Category.Name,
            attribute.Key,
            attribute.Type,
            attribute.Required,
            attribute.Min,
            attribute.Max
        );
    }
}
