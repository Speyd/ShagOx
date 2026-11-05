using ShagOxServer.Application.Interfaces.Services.Common.Currencies;

namespace ShagOxServer.Infrastructure.ExternalServices.Currencies;
public class CurrencyConverter
    : ICurrencyConverter
{
    private readonly CurrencyRateStore _rateStore;

    public CurrencyConverter(CurrencyRateStore rateStore)
    {
        _rateStore = rateStore;
    }

    public decimal Convert(
        decimal amount,
        string from,
        string to)
    {
        var fromRate = _rateStore.GetRate(from);
        var toRate = _rateStore.GetRate(to);

        return amount * toRate / fromRate;
    }
}
