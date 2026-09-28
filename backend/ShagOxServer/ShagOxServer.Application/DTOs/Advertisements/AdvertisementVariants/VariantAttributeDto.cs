namespace ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants;
public sealed record VariantAttributeDto
(
    long AttributeId,
    string AttributeKey,
    long ValueId,
    string ValueCode,
    string? Value,
    string ValueLabel
);