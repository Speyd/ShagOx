using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query.Core;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Query;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Application.DTOs.Specification.Pictures.AdvertisementImages.Query;
using ShagOxServer.Domain.Base.Enum;
using System.Text.Json;

namespace ShagOxServer.Application.DTOs.Advertisements.Core.Query.LineItems;
public sealed record AdvertisementLineItemDto
(
    long Id,
    EntityStatus EntityStatus,
    string Title,
    string Description,
    decimal? Price,
    decimal? PreviousPrice,
    ConvertedPricesDto PriceConversion,
    long CurrencyId,
    long CategoryId,
    long SellerId,
    long? BuyerId,
    StatusDto Status,
    AdvertisementImageDto? Picture,
    JsonDocument Attributes,
    AdvertisementVariantShortDto? Variant,
    DateTime? SoldAt,
    DateTime CreatedAt
) : BaseDto(Id, EntityStatus);
