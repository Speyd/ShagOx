using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.DTOs.Advertisement.Add;
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