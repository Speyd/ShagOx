using ShagOxServer.Application.DTOs.Base;
using System.Text.Json;

namespace ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
public sealed record AdvertisementVariantShortDto
(
    long Id,
    long AdvertisementId,
    decimal Price,
    decimal PreviousPrice,
    int Stock,
    JsonDocument Attributes
) : BaseDto(Id);