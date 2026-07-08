
namespace ShagOxServer.Application.DTOs.Specification.Currencies;
public sealed record CurrencyDto
(
    int Id,
    string Code,
    string Symbol,
    string Name
);