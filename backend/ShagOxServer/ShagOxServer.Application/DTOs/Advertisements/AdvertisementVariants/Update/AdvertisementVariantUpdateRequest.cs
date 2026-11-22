using System.Text.Json;

namespace ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Update;
public sealed record AdvertisementVariantUpdateRequest
(
    decimal? Price,
    int? Stock,
    JsonDocument? Attributes
);
