namespace ShagOxServer.Application.DTOs.Specification.Images.Create;
public sealed record ImageCreateRequest
(
    string Url,
    int Order,
    int AdvertisementId
);