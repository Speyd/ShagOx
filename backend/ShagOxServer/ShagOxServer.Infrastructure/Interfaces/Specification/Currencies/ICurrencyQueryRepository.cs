using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Infrastructure.Interfaces.Specification.Currencies;
public interface ICurrencyQueryRepository
{
    Task<Currency?> GetByIdAsync(int id);

    Task<Currency?> GetByCodeAsync(string code);

    Task<Currency?> GetBySymbolAsync(string symbol);
}
