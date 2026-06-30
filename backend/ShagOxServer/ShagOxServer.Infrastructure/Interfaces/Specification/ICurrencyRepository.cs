using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Infrastructure.Interfaces.Specification;
public interface ICurrencyRepository
{
    Task AddAsync(Currency currency);

    Task<bool> UpdateAsync(Currency currency);

    Task DeleteAsync(Currency currency);


    Task<Currency?> GetByCodeAsync(string code);

    Task<Currency?> GetBySymbolAsync(string symbol);

    Task<Currency?> GetByIdAsync(int id);



    Task<bool> ExistsByCodeAsync(string code);

    Task<bool> ExistsBySymbolAsync(string symbol);

    Task<bool> ExistsByNameAsync(string name);   
}