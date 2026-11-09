using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Query;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Application.DTOs.Specification.Pictures.AdvertisementImages.Query;
using ShagOxServer.Domain.Base.Enum;
using System.Text.Json;

namespace ShagOxServer.Application.DTOs.Advertisements.Core.Query;
public sealed record AdvertisementShortDto
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
    AdvertisementImageDto? Image,
    JsonDocument Attributes,
    List<AdvertisementVariantShortDto> Variants,
    DateTime? SoldAt,
    DateTime CreatedAt
) : BaseDto(Id, EntityStatus);
