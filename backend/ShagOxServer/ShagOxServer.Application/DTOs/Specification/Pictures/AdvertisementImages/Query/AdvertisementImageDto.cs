namespace ShagOxServer.Application.DTOs.Specification.Pictures.AdvertisementImages.Query;
public sealed record AdvertisementImageDto(
    long Id,
    string Url,
    string PublicId,
    int Order,
    long AdvertisementId
) : BaseImageDto(Id, Url, PublicId);
