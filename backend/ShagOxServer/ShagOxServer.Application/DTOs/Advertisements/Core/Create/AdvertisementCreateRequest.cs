using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.DTOs.Advertisements.Core.Create;
public sealed record AdvertisementCreateRequest
(
    string Title,
    string Description,
    int Popularity,
    long CurrencyId,
    long ConditionId,
    long CategoryId,
    long StatusId,
    List<IFormFile> Pictures,
    string? Attributes,
    string? Variants
);
