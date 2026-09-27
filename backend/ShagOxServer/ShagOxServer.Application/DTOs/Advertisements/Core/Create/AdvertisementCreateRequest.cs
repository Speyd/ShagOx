using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace ShagOxServer.Application.DTOs.Advertisements.Core.Create;
public sealed record AdvertisementCreateRequest
(
    string Title,
    string Description,
    int Popularity,
    long CurrencyId,
    long ConditionId,
    long CategoryId,
    List<IFormFile> Images,
    JsonDocument? Attributes = null
    //List<AdvertisementVariantCreateRequest>? Values = null
);