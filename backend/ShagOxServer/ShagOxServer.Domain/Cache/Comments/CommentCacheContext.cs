using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Comments;
public sealed record CommentCacheContext
(
    long Id,
    long CommentableEntityId,
    EntityStatus Status = EntityStatus.Active
);
