using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Specification.Currencies.Query;
public sealed record CurrencyDto
(
    long Id,
    EntityStatus EntityStatus,
    string Code,
    string Symbol,
    string Name
) : BaseDto(Id, EntityStatus);
