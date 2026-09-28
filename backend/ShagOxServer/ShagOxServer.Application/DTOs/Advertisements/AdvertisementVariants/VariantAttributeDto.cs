using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues;

namespace ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants;
public sealed record VariantAttributeDto
(
    long AttributeId,
    string AttributeKey,
    AttributeDictionaryValueDto Value
);