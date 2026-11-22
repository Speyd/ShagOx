namespace ShagOxServer.Application.Common.Options.Currencies;
public class CurrencyRatesUpdaterOptions
{
    public const string SectionName = "CurrencyRatesUpdater";

    public TimeSpan UpdateInterval { get; init; }

    public string RatesFilePath { get; init; } = "";
    public string RatesFileName { get; init; } = "";

}
