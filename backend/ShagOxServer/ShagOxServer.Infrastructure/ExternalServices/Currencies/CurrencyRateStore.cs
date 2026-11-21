namespace ShagOxServer.Infrastructure.ExternalServices.Currencies;
public class CurrencyRateStore
{
    private volatile CurrencyRatesSnapshot? _snapshot;

    public CurrencyRatesSnapshot? Snapshot => _snapshot;

    public void Update(CurrencyRatesSnapshot snapshot)
    {
        _snapshot = snapshot;
    }

    public bool TryGetRate(string currencyCode, out decimal rate)
    {
        rate = default;

        var snapshot = _snapshot;

        return snapshot is not null
            && snapshot.Rates.TryGetValue(currencyCode, out rate);
    }
}
