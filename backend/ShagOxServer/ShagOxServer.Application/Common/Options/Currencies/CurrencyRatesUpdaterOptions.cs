namespace ShagOxServer.Application.Common.Options.Currencies;
public class CurrencyRatesUpdaterOptions
{
    public const string SectionName = "CurrencyRatesUpdater";

    public TimeSpan UpdateInterval { get; init; }
}
