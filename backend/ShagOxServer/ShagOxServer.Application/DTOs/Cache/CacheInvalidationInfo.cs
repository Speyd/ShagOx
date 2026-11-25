using ShagOxServer.Application.DTOs.Cache.Enum;

namespace ShagOxServer.Application.DTOs.Cache;
public record CacheInvalidationInfo
(
    Type EntityType,
    object EntityId,
    CacheInvalidationType Type
);
