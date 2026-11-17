namespace ShagOxServer.Application.Interfaces.Services.Common.Currencies;
public interface ICurrencyConverter
{
    public bool TryConvert(
        decimal? amount,
        string from,
        string to,
        out decimal? convertedAmount,
        out decimal coefficient);

    public (decimal? ConvertedAmount, decimal Coefficient) Convert(
        decimal? amount,
        string from,
        string to);
}
