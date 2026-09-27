using ShagOxServer.Application.DTOs.Base;
using System.Text.Json;

namespace ShagOxServer.Application.DTOs.Advertisements;
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
    DateTime? SoldAt,
    DateTime CreatedAt
) : BaseDto(Id);