namespace ShagOxServer.Application.Common.Options.Currencies;
public class CurrencyRatesUpdaterOptions
{
    public TimeSpan UpdateInterval { get; init; }

    public TimeSpan RetryInterval { get; init; } = TimeSpan.FromMinutes(30);

    public string CacheFilePath { get; init; } = ".cache/currency-rates.json";
}
