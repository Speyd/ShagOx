namespace ShagOxServer.Application.DTOs.Specification.Pictures.Images;
public sealed record ImageDto(
    int Id,
    string Url,
    string PublicId,
    int Order,
    int AdvertisementId
) : BaseImageDto(Id, Url, PublicId);