namespace ShagOxServer.Application.DTOs.Advertisements.Update;
public sealed record AdvertisementUpdateRequest
(
    int Id,
    string? Title = null,
    string? Description = null,
    int? Price = null,
    int? Popularity = null,
    int? CurrencyId = null,
    int? CategoryId = null,
    Dictionary<string, string>? Properties = null
);