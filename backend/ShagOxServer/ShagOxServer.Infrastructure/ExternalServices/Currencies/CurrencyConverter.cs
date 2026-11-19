using ShagOxServer.Application.DTOs.Deliveries;
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

    public bool TryConvert(
        decimal? amount,
        string from,
        string to,
        out decimal? convertedAmount,
        out decimal coefficient)
    {
        convertedAmount = amount;
        coefficient = 1m;

        if (string.Equals(from, to, StringComparison.OrdinalIgnoreCase))
            return true;

        if (!_rateStore.TryGetRate(from, out var fromRate)
            || !_rateStore.TryGetRate(to, out var toRate)
            || fromRate == 0)
            return false;

        coefficient = toRate / fromRate;
        convertedAmount = amount * coefficient;

        return true;
    }

    public AmountConversionResult Convert(
        decimal? amount,
        string from,
        string to)
    {
        return TryConvert(amount, from, to, out var converted, out var coefficient)
            ? new AmountConversionResult(converted, coefficient)
            : new AmountConversionResult(null, 0m);
    }
}
