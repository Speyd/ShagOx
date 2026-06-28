using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Infrastructure.Interfaces.Specification;
public interface ICurrencyRepository
{
    Task<Currency?> GetByCodeAsync(string code);

    Task<Currency?> GetBySymbolAsync(string symbol);

    Task<Currency?> GetByIdAsync(int id);

    Task AddAsync(Currency currency);

    Task<bool> ExistsCodeAsync(string code);

    Task<bool> ExistsSymbolAsync(string symbol);

    Task<bool> UpdateAsync(Currency currency);
}
