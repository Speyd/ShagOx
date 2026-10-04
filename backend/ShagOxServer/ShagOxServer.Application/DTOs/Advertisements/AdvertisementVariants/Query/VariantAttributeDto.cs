using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Query;

namespace ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
public sealed record VariantAttributeDto
(
    long AttributeId,
    string AttributeKey,
    AttributeDictionaryValueDto Value
);