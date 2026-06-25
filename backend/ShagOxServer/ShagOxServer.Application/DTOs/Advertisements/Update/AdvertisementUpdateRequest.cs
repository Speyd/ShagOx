namespace ShagOxServer.Application.DTOs.Advertisements.Update;
public sealed record AdvertisementUpdateRequest
(
    string? Title = null,
    string? Description = null,
    int? Price = null,
    int? CurrencyId = null,
    int? CategoryId = null,
    Dictionary<string, string>? Property = null,
    int? Popularity = null
);