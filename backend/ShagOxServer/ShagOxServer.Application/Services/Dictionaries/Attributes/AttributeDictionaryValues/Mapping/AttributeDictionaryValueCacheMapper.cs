using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Cache;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Mapping;
public static class AttributeDictionaryValueCacheMapper
{
    public static AttributeDictionaryValueCacheInfo ToInfo(
        AttributeDictionaryValue value)
    {
        return new AttributeDictionaryValueCacheInfo(
            value.Id,
            value.DictionaryId
        );
    }
}