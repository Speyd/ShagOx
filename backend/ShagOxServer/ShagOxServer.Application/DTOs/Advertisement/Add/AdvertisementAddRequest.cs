using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.DTOs.Advertisement.Add;
public sealed record AdvertisementAddRequest
(
    string Title,
    int Price,
    int CurrencyId,
    int CategoryId,
    int SellerId,
    string? Description = null,
    int Populrity = 0
);