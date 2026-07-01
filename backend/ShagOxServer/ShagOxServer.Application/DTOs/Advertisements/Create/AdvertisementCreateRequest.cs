namespace ShagOxServer.Application.DTOs.Advertisements.Create;
public sealed record AdvertisementCreateRequest
(
    string Title,
    string Description,
    int Popularity,
    int Price,
    int CurrencyId,
    int ConditionId,
    int CategoryId,
    int SellerId,
    List<int> Images,
    Dictionary<string, string>? Properties = null
);