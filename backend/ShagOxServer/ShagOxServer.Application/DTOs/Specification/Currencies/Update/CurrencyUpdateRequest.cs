
namespace ShagOxServer.Application.DTOs.Specification.Currencies.Update;
public sealed record CurrencyUpdateRequest
(
    string? Code,
    string? Symbol,
    string? Name
);
