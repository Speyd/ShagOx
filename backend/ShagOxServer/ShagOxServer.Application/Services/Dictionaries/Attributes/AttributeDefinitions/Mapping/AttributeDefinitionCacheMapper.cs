using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Mapping;
public static class AttributeDefinitionCacheMapper
{
    public static AttributeDefinitionCacheInfo ToInfo(
        AttributeDefinition value,
        EntityStatus status = EntityStatus.Active)
    {
        return new AttributeDefinitionCacheInfo(
            value.Id,
            value.CategoryId,
            status
        );
    }
}
