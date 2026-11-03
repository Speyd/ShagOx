using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Specification.Pictures.Review.ReviewRepliImages.Query;
public sealed record ReviewRepliImageDto(
    long Id,
    EntityStatus EntityStatus,
    string Url,
    string PublicId,
    int Order,
    long ReviewRepliCommentId
) : BaseImageDto(Id, EntityStatus, Url, PublicId);
