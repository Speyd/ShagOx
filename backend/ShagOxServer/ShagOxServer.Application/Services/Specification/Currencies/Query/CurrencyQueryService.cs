using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Specification.Currencies.Query;
using ShagOxServer.Application.Services.Specification.Currencies.Mapping;
using ShagOxServer.Infrastructure.Interfaces.Specification.Currencies;

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
        if (currency is null)
            Result<CurrencyDto>.NotFound("Currency");

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

    public async Task<Result<List<CurrencyDto>>> SearchByCode(
     string code,
     int page,
     int pageSize)
    {
        var currencies = await _repository.SearchByCode(code, page, pageSize);

        return currencies.ToResultList(CurrencyMapper.ToDto);
    }

    public async Task<Result<List<CurrencyDto>>> SearchByName(
      string name,
      int page,
      int pageSize)
    {
        var currencies = await _repository.SearchByName(name, page, pageSize);

        return currencies.ToResultList(CurrencyMapper.ToDto);
    }
}
