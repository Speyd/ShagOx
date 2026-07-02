using ShagOxServer.Application.DTOs.Specification.Currencies;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Specification.Currencies.Query;
public interface ICurrencyQueryService
{
    Task<Result<CurrencyDto>> GetByIdAsync(int id);

    Task<Result<CurrencyDto>> GetByCodeAsync(string code);

    Task<Result<CurrencyDto>> GetBySymbolAsync(string symbol);

    Task<Result<List<CurrencyDto>>> SearchByCode(
      string code,
	  PaginationParams pagination);

    Task<Result<List<CurrencyDto>>> SearchByName(
      string name,
	  PaginationParams pagination);
}
