using ShagOxServer.Application.DTOs.Base;
using System.Text.Json;

namespace ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants;
public sealed record AdvertisementVariantDto
(
    long Id,
    long AdvertisementId,
    decimal Price,
    decimal PreviousPrice,
    int Stock,
    JsonDocument Attributes
) : BaseDto(Id);