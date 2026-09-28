using System.Text.Json;

namespace ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Create;
public sealed record AdvertisementVariantCreateRequest
(
    long AdvertisementId,
    decimal Price,
    decimal PreviousPrice,
    int Stock,
    JsonDocument Attributes
);