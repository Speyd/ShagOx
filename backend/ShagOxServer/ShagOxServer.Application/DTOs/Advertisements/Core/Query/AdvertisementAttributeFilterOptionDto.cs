namespace ShagOxServer.Application.DTOs.Advertisements.Core.Query;

public sealed record AdvertisementAttributeFilterOptionDto(
    string Key,
    string Value,
    bool IsVariant);
