using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
public sealed record PictureCacheInfo
(
    long Id,
    long OwnerId,
    EntityStatus Status = EntityStatus.Active
);
