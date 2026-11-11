using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Domain.Base.Enum;
using System.Text.Json;

namespace ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query.Core;
public sealed record AdvertisementVariantShortDto
(
    long Id,
    EntityStatus EntityStatus,
    long AdvertisementId,
    decimal Price,
    decimal PreviousPrice,
    ConvertedPricesDto PriceConversion,
    int Stock,
    JsonDocument Attributes
) : BaseDto(Id, EntityStatus);
