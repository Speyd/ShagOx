using System.Text.Json;

namespace ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Update;
public sealed record AdvertisementVariantUpdateRequest
(
    long? AdvertisementId,
    decimal? Price,
    int? Stock,
    JsonDocument? Attributes
);