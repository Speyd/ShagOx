using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Currencies;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
public interface ICurrencyQueryRepository
    : ISearchRepository<Currency, CurrencySearchFilter>
{
    Task<Currency?> GetByCodeAsync(
        string code);

    Task<Currency?> GetBySymbolAsync(
        string symbol);
}
