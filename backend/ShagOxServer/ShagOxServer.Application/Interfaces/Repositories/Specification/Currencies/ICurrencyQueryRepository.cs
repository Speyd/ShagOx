using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Currencies;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
public interface ICurrencyQueryRepository
    : IQueryRepository<Currency>
{
    Task<Currency?> GetByCodeAsync(string code);

    Task<Currency?> GetBySymbolAsync(string symbol);

    Task<PagedResult<Currency>> Search(
      CurrencySearchFilter filter,
      PaginationParams pagination);
}