using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Currencies;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Interfaces.Specification.Currencies;
public interface ICurrencyQueryRepository
{
    Task<Currency?> GetByIdAsync(int id);

    Task<Currency?> GetByCodeAsync(string code);

    Task<Currency?> GetBySymbolAsync(string symbol);

    Task<List<Currency>> Search(
      CurrencySearchFilter filter,
      PaginationParams pagination);
}
