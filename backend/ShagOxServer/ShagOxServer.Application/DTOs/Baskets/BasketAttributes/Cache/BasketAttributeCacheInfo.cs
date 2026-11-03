using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Cache;
public sealed record BasketAttributeCacheInfo
(
    long Id,
    long CategoryId,
    long AttributeDefinitionId,
    EntityStatus Status = EntityStatus.Active
);
