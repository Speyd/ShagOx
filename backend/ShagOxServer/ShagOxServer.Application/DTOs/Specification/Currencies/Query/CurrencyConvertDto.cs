namespace ShagOxServer.Application.DTOs.Specification.Currencies.Query;

public sealed record CurrencyConvertDto
(
    CurrencyShortDto From,
    CurrencyShortDto To,
    decimal Amount
);
