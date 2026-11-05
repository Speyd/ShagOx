namespace ShagOxServer.Application.Interfaces.Services.Common.Currencies;
public interface ICurrencyConverter
{
    public (decimal? ConvertedAmount, decimal Coefficient) Convert(
        decimal? amount,
        string from,
        string to);
}
