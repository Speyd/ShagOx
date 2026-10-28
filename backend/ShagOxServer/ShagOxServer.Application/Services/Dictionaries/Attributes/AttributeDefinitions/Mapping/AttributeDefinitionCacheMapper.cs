using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Cache;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Mapping;
public static class AttributeDefinitionCacheMapper
{
    public static AttributeDefinitionCacheInfo ToInfo(
        AttributeDefinition value)
    {
        return new AttributeDefinitionCacheInfo(
            value.Id,
            value.CategoryId
        );
    }
}
