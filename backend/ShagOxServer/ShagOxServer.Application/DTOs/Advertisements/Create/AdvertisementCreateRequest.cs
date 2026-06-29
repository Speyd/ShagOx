namespace ShagOxServer.Application.DTOs.Advertisements.Create;
public sealed record AdvertisementCreateRequest
(
    string Title,
    int Price,
    int CurrencyId,
    int CategoryId,
    int SellerId,
    Dictionary<string, string>? Properties = null,
    string? Description = null,
    int Popularity = 0
);