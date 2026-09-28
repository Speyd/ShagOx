using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using System.Text.Json;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
public static class VariantAttributeMapper
{
    public static VariantAttributeDto ToSelectDto(
    AttributeDefinition attribute,
    AttributeDictionaryValue value)
    {
        return new VariantAttributeDto(
            attribute.Key,
            value.Id,
            value.Code,
            value.Value,
            //TODO: доделать
            ""
        );
    }

    public static VariantAttributeDto ToDto(
    AttributeDefinition attribute,
    JsonElement value)
    {
        return new VariantAttributeDto(
            attribute.Key,
            0,
            "",
            value.ToString(),
            ""
        );
    }
}