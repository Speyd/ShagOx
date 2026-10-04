using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Mapping;
public static class AttributeDictionaryValueMapper
{
    public static AttributeDictionaryValueDto ToDto(
        AttributeDictionaryValue value,
        string? label)
    {
        return new AttributeDictionaryValueDto(
            value.Id,
            value.DictionaryId,
            value.Code,
            value.Value,
            label
        );
    }
}