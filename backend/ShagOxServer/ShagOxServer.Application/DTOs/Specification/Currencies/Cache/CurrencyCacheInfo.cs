using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Specification.Currencies.Cache;
public sealed record CurrencyCacheInfo
(
    long Id,
    string Code,
    string Symbol,
    EntityStatus Status = EntityStatus.Active
);
