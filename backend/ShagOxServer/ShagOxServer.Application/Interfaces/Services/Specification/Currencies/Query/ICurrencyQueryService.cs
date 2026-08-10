using ShagOxServer.Application.DTOs.Auth.Users;
using ShagOxServer.Application.DTOs.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Filters.Specification.Currencies;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Query;
public interface ICurrencyQueryService
    : IQueryService<CurrencyDto>
{
    Task<Result<CurrencyDto>> GetByCodeAsync(string code);

    Task<Result<CurrencyDto>> GetBySymbolAsync(string symbol);

    Task<Result<PagedResult<CurrencyDto>>> Search(
      CurrencySearchFilter filter,
	  PaginationParams pagination);
}