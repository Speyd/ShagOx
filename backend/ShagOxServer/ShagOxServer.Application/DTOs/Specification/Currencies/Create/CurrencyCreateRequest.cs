
namespace ShagOxServer.Application.DTOs.Specification.Currencies.Create;
public sealed record CurrencyCreateRequest
(
    string Code,
    string Symbol,
    string Name
);
