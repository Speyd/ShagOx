using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Specification.Currencies.Query;
public sealed record CurrencyDto
(
    long Id,
    string Code,
    string Symbol,
    string Name
) : BaseDto(Id);