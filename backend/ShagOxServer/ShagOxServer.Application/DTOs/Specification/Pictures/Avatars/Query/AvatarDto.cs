using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Query;
public sealed record AvatarDto(
    long Id,
    EntityStatus EntityStatus,
    string Url,
    string PublicId,
    long UserId
) : BaseImageDto(Id, EntityStatus, Url, PublicId);
