using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.DTOs.Base;
using System.Text.Json;

namespace ShagOxServer.Application.DTOs.Advertisements.Core.Query;
public sealed record AdvertisementShortDto
(
    long Id,
    string Title,
    string Description,
    long CurrencyId,
    long CategoryId,
    long SellerId,
    long? BuyerId,
    List<long> ImageIds,
    JsonDocument Attributes,
    List<AdvertisementVariantShortDto> Variants,
    DateTime? SoldAt,
    DateTime CreatedAt
) : BaseDto(Id);