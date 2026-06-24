namespace ShagOxServer.Application.DTOs.Advertisements.Create;
public sealed record AdvertisementAddRequest
(
    string Title,
    int Price,
    int CurrencyId,
    int CategoryId,
    int SellerId,
    Dictionary<string, string> Property,
    string? Description = null,
    int Popularity = 0
);