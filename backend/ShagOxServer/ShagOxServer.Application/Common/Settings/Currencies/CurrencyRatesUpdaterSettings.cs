namespace ShagOxServer.Application.Common.Settings.Currencies;
public class CurrencyRatesUpdaterSettings
{
    public TimeSpan UpdateInterval { get; init; }

    public TimeSpan RetryInterval { get; init; } = TimeSpan.FromMinutes(30);

    public string CacheFilePath { get; init; } = ".cache/currency-rates.json";
}
