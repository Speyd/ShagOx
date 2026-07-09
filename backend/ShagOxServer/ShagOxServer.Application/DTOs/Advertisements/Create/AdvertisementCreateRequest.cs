using Microsoft.AspNetCore.Http;

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
    List<IFormFile> Images,
    Dictionary<string, string>? Properties = null
);