namespace ShagOxServer.Application.DTOs.Advertisements.Update;
public sealed record AdvertisementUpdateRequest
(
    string? Title,
    string? Description,
    int? Popularity,
    int? Price,
    int? CurrencyId,
    int? ConditionId,
    int? CategoryId,
    int? BuyerId,
    List<int>? Images,
    Dictionary<string, string>? Properties
);