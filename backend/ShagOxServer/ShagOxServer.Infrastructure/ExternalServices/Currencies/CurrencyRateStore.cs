using ShagOxServer.Application.DTOs.Common.Currencies.Fx;

namespace ShagOxServer.Infrastructure.ExternalServices.Currencies;
public class CurrencyRateStore
{
    private Dictionary<string, decimal> _rates = new();

    public IReadOnlyDictionary<string, decimal> Rates => _rates;

    public void Update(FxApiResponse response)
    {
        _rates = response.Data.ToDictionary(
            x => x.Key,
            x => x.Value.Value);
    }

    public decimal GetRate(string currencyCode)
    {
        if (!_rates.TryGetValue(currencyCode, out var rate))
            throw new KeyNotFoundException(
                $"Rate for {currencyCode} not found.");

        return rate;
    }
}
