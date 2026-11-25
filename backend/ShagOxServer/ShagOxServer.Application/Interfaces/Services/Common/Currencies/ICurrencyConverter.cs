using ShagOxServer.Application.DTOs.Deliveries;

namespace ShagOxServer.Application.Interfaces.Services.Common.Currencies;
public interface ICurrencyConverter
{
    public AmountConversionResult Convert(
        decimal? amount,
        string from,
        string to);
}
