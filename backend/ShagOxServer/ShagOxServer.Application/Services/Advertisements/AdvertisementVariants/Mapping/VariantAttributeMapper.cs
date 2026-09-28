using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using System.Text.Json;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
public static class VariantAttributeMapper
{
    public static VariantAttributeDto ToSelectDto(
        AttributeDefinition attribute,
        AttributeDictionaryValueDto value)
    {
        return new VariantAttributeDto(
            attribute.Id,
            attribute.Key,
            value
        );
    }

    public static VariantAttributeDto ToDto(
        AttributeDefinition attribute,
        JsonElement value)
    {
        return new VariantAttributeDto(
            attribute.Id,
            attribute.Key,
            new AttributeDictionaryValueDto(
                0,
                0,
                "",
                value.ToString(),
                ""
                )
        );
    }
}