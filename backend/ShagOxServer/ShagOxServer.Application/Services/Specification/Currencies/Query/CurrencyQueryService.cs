using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Specification.Currencies.Query;
using ShagOxServer.Application.Services.Specification.Currencies.Mapping;
using ShagOxServer.Infrastructure.Interfaces.Specification.Currencies;

namespace ShagOxServer.Application.Services.Specification.Currencies.Query;
public class CurrencyQueryService : ICurrencyQueryService
{
    private readonly ICurrencyRepository _repository;

    public CurrencyQueryService(
        ICurrencyRepository currencyRepository)
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
        if (currency is null)
            Result<CurrencyDto>.NotFound("Currency");

        return currency.ToResult(CurrencyMapper.ToDto);
    }

    public async Task<Result<CurrencyDto>> GetBySymbolAsync(string symbol)
    {
        var currency = await _repository.GetBySymbolAsync(symbol);
        if (currency is null)
            Result<CurrencyDto>.NotFound("Currency");

        return currency.ToResult(CurrencyMapper.ToDto);
    }
}
