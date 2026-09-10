using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.DTOs.Advertisements.Core.Create;
public sealed record AdvertisementCreateRequest
(
    string Title,
    string Description,
    int Stock,
    int Popularity,
    int Price,
    int CurrencyId,
    int ConditionId,
    int CategoryId,
    List<IFormFile> Images,
    Dictionary<string, string>? Properties = null
);