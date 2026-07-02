using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Infrastructure.Interfaces.Specification.Currencies;
public interface ICurrencyQueryRepository
{
    Task<Currency?> GetByIdAsync(int id);

    Task<Currency?> GetByCodeAsync(string code);

    Task<Currency?> GetBySymbolAsync(string symbol);

    Task<List<Currency>> SearchByCode(
      string code,
      int page,
      int pageSize);

    Task<List<Currency>> SearchByName(
      string name,
      int page,
      int pageSize);
}
