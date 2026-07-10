using Microsoft.AspNetCore.Http;

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
    List<IFormFile>? NewImages,
    List<int>? DeletedImageIds,
    Dictionary<string, string>? Properties
);