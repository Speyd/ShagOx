using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using System.Text.Json;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeValues.Mapping;
public static class AttributeValueMapper
{
    public static AttributeDictionaryValueDto ToDto(
        JsonElement value)
    {
        return new AttributeDictionaryValueDto(
            0,
            0,
            "",
            value.ToString(),
            ""
        );
    }

    public static List<AttributeDictionaryValueDto> ToDto(
        List<JsonElement> values)
    {
        return values
            .Select(
                value => ToDto(value))
            .ToList();
    }
}
