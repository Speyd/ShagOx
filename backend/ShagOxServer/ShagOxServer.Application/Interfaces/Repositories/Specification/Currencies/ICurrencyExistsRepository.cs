namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
public interface ICurrencyExistsRepository
{
    Task<bool> ExistsByIdAsync(int id);

    Task<bool> ExistsByCodeAsync(string code);

    Task<bool> ExistsBySymbolAsync(string symbol);

    Task<bool> ExistsByNameAsync(string name);
}