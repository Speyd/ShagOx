using ShagOxServer.Application.DTOs.Specification.Currencies;
using ShagOxServer.Domain.Filters.Specification.Currencies;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Query;
public interface ICurrencyQueryService
{
    Task<Result<CurrencyDto>> GetByIdAsync(int id);

    Task<Result<PagedResult<CurrencyDto>>> GetPagedAsync(
        PaginationParams pagination);

    Task<Result<CurrencyDto>> GetByCodeAsync(string code);

    Task<Result<CurrencyDto>> GetBySymbolAsync(string symbol);

    Task<Result<PagedResult<CurrencyDto>>> Search(
      CurrencySearchFilter filter,
	  PaginationParams pagination);
}