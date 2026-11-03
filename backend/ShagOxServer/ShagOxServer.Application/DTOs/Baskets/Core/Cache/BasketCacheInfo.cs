using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Baskets.Core.Cache;
public sealed record BasketCacheInfo
(
    long Id,
    long UserId,
    EntityStatus Status = EntityStatus.Active
);
