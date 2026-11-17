using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.DTOs.Advertisements.Core.Create;
public sealed record AdvertisementCreateRequest
(
    string Title,
    string Description,
    decimal? Price,
    decimal? PreviousPrice,
    int? Stock,
    int Popularity,
    long CurrencyId,
    long ConditionId,
    long CategoryId,
    long StatusId,
    List<IFormFile> Pictures,
    string? Attributes,
    string? Variants
);
