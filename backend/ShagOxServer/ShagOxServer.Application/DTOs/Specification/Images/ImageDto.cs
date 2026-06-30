namespace ShagOxServer.Application.DTOs.Specification.Images;
public sealed record ImageDto
(
    int Id,
    string Url,
    int Order,
    int AdvertisementId,
    string AdvertisementName
);