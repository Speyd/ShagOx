namespace ShagOxServer.Application.DTOs.Specification.Pictures.Images.Create;
public sealed record ImageCreateRequest
(
    string Url,
    int Order,
    int AdvertisementId
);