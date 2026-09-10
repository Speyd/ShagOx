using ShagOxServer.Application.DTOs.Advertisements.Core.Update.Images;

namespace ShagOxServer.Application.DTOs.Advertisements.Core.Update;
public sealed record AdvertisementUpdateRequest
(
    string? Title,
    string? Description,
    int? Stock,
    int? Popularity,
    int? Price,
    int? CurrencyId,
    int? ConditionId,
    int? CategoryId,
    int? BuyerId,
    List<ImageAdvertUpdateRequest>? Images,
    Dictionary<string, string>? Properties
);