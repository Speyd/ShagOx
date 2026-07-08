using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Infrastructure.Interfaces.Specification.Currencies;
public interface ICurrencyRepository
{
    Task<Currency?> GetByIdAsync(int id);

    Task AddAsync(Currency currency);

    Task<bool> UpdateAsync(Currency currency);

    Task DeleteAsync(Currency currency);
}