namespace ShagOxServer.Application.DTOs.Specification.Pictures.Images.Query;
public sealed record ImageDto(
    long Id,
    string Url,
    string PublicId,
    int Order,
    long AdvertisementId
) : BaseImageDto(Id, Url, PublicId);