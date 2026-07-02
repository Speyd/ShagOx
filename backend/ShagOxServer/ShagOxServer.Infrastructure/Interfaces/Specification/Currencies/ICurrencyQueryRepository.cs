using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Interfaces.Specification.Currencies;
public interface ICurrencyQueryRepository
{
    Task<Currency?> GetByIdAsync(int id);

    Task<Currency?> GetByCodeAsync(string code);

    Task<Currency?> GetBySymbolAsync(string symbol);

    Task<List<Currency>> SearchByCode(
      string code,
      PaginationParams pagination);

    Task<List<Currency>> SearchByName(
      string name,
      PaginationParams pagination);
}
