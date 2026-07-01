namespace ShagOxServer.Application.DTOs.Specification.Images.Update;
public sealed record  CurrencyUpdateRequest
(
    string? Url,
    int? Order,
    int? AdvertisementId
);