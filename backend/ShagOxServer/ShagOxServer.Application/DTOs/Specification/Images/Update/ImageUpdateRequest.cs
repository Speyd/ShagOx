namespace ShagOxServer.Application.DTOs.Specification.Images.Update;
public sealed record  ImageUpdateRequest
(
    string? Url,
    int? Order,
    int? AdvertisementId
);