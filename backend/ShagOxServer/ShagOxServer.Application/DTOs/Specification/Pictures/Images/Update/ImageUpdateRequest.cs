namespace ShagOxServer.Application.DTOs.Specification.Pictures.Images.Update;
public sealed record  ImageUpdateRequest
(
    string? Url,
    int? Order,
    long? AdvertisementId
);