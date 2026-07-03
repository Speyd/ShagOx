namespace ShagOxServer.Domain.Filters.Specification.Currencies;
public sealed record CurrencySearchFilter
(
    string? Code,
    string? Symbol,
    string? Name
);
