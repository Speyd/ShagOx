using ShagOxServer.Application.DTOs.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Specification.Currencies.Query;
using ShagOxServer.Application.Services.Specification.Currencies.Mapping;
using ShagOxServer.Domain.Filters.Specification.Currencies;
using ShagOxServer.Infrastructure.Interfaces.Specification.Currencies;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Specification.Currencies.Query;
public class CurrencyQueryService : ICurrencyQueryService
{
    private readonly ICurrencyQueryRepository _repository;

    public CurrencyQueryService(
        ICurrencyQueryRepository currencyRepository)
    {
        _repository = currencyRepository;
    }

    public async Task<Result<CurrencyDto>> GetByCodeAsync(string code)
    {
        var currency = await _repository.GetByCodeAsync(code);

        return currency.ToResult(CurrencyMapper.ToDto);
    }

    public async Task<Result<CurrencyDto>> GetByIdAsync(int id)
    {
        var currency = await _repository.GetByIdAsync(id);

        return currency.ToResult(CurrencyMapper.ToDto);
    }

    public async Task<Result<CurrencyDto>> GetBySymbolAsync(string symbol)
    {
        var currency = await _repository.GetBySymbolAsync(symbol);

        return currency.ToResult(CurrencyMapper.ToDto);
    }

    public async Task<Result<List<CurrencyDto>>> Search(
     CurrencySearchFilter filter,
	 PaginationParams pagination)
    {
        var currencies = await _repository.Search(filter, pagination);

        return currencies.ToResultList(CurrencyMapper.ToDto);
    }
}
