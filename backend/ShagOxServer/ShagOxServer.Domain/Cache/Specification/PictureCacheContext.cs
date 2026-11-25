using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Specification;
public sealed record PictureCacheContext
(
    long Id,
    long OwnerId,
    EntityStatus Status = EntityStatus.Active
);
