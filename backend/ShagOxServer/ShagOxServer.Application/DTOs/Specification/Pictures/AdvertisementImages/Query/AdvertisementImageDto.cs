using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Specification.Pictures.AdvertisementImages.Query;
public sealed record AdvertisementImageDto(
    long Id,
    EntityStatus EntityStatus,
    string Url,
    string PublicId,
    int Order,
    long AdvertisementId
) : BaseImageDto(Id, EntityStatus, Url, PublicId);
