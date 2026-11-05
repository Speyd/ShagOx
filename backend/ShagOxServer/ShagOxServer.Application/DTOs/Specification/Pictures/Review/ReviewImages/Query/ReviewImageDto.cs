using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Specification.Pictures.Review.ReviewImages.Query;
public sealed record ReviewImageDto(
    long Id,
    EntityStatus EntityStatus,
    string Url,
    string PublicId,
    int Order,
    long ReviewCommentId
) : BaseImageDto(Id, EntityStatus, Url, PublicId);
