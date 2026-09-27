using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Mapping;
public static class AttributeDictionaryValueMapper
{
    public static AttributeDictionaryValueDto ToDto(
        AttributeDictionaryValue value)
    {
        return new AttributeDictionaryValueDto(
            value.Id,
            value.DictionaryId,
            value.Code,
            value.Value
        );
    }
}