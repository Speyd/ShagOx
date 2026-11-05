namespace ShagOxServer.Application.Interfaces.Services.Common.Currencies;
public interface ICurrencyConverter
{
    public decimal Convert(
        decimal amount,
        string from,
        string to);
}
