using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
public interface ICurrencyRepository
{
    Task<Currency?> GetByIdAsync(int id);

    void Add(Currency currency);

    bool Update(Currency currency);

    void Delete(Currency currency);
}