namespace ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants;
public sealed record VariantAttributeDto
(
    string AttributeKey,
    long ValueId,
    string ValueCode,
    string? Value,
    string ValueLabel
);