using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.CategoryAttributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using System.Text.Json;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
public static class VariantAttributeMapper
{
    public static AttributeDto ToSelectDto(
        AttributeDefinition attribute,
        AttributeDictionaryValueDto value)
    {
        return new AttributeDto(
            attribute.Id,
            attribute.Key,
            new List<AttributeDictionaryValueDto> { value }
        );
    }

    public static AttributeDto ToSelectDto(
        AttributeDefinition attribute,
        List<AttributeDictionaryValueDto> values)
    {
        return new AttributeDto(
            attribute.Id,
            attribute.Key,
            values
        );
    }

    public static AttributeDto ToDto(
        AttributeDefinition attribute,
        JsonElement value)
    {
        return new AttributeDto(
            attribute.Id,
            attribute.Key,
            new List<AttributeDictionaryValueDto> {
                new AttributeDictionaryValueDto(
                    0,
                    0,
                    "",
                    value.ToString(),
                    ""
                    ) 
            }
        );
    }

    public static AttributeDto ToDto(
        AttributeDefinition attribute,
        List<JsonElement> values)
    {
        return new AttributeDto(
            attribute.Id,
            attribute.Key,
             values
            .Select(value => new AttributeDictionaryValueDto(
                0,
                0,
                "",
                value.ToString(),
               ""))
            .ToList()
        );
    }
}
