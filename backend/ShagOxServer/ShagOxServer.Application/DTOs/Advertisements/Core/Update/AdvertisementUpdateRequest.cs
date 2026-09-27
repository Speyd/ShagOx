using ShagOxServer.Application.DTOs.Advertisements.Core.Update.Images;
using System.Text.Json;

namespace ShagOxServer.Application.DTOs.Advertisements.Core.Update;
public sealed record AdvertisementUpdateRequest
(
    string? Title,
    string? Description,
    int? Popularity,
    long? CurrencyId,
    long? ConditionId,
    long? CategoryId,
    long? BuyerId,
    List<ImageAdvertUpdateRequest>? Images,
    JsonDocument? Attributes
);