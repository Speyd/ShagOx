using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Comments.Core.Cache;
public sealed record CommentCacheInfo
(
    long Id,
    long CommentableEntityId,
    EntityStatus Status = EntityStatus.Active
);
