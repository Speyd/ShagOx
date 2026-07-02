namespace ShagOxServer.Infrastructure.Interfaces.Specification.Currencies;
public interface ICurrencyExistsRepository
{
    Task<bool> ExistsByCodeAsync(string code);

    Task<bool> ExistsBySymbolAsync(string symbol);

    Task<bool> ExistsByNameAsync(string name);
}