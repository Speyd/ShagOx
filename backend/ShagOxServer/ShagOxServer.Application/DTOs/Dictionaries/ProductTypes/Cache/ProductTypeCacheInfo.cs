using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Cache;
public sealed record ProductTypeCacheInfo
(
    long Id,
    EntityStatus Status = EntityStatus.Active
);
