namespace ShagOxServer.Infrastructure.ExternalServices.Currencies;
public sealed record CurrencyRatesSnapshot(
    IReadOnlyDictionary<string, decimal> Rates,
    DateTimeOffset UpdatedAt);
