using ShagOxServer.Application.DTOs.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Specification.Currencies.Mapping;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Currencies;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Specification.Currencies.Query;
public class CurrencyQueryService 
    : BaseQueryService<
        CurrencyDto,
        Currency,
        CurrencySearchFilter
        >,
    ICurrencyQueryService
{
    private readonly ICurrencyQueryRepository _currencyRepository;


    public CurrencyQueryService(
        ICurrencyQueryRepository currencyRepository
    )
        : base(currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }


    public override async Task<CurrencyDto> ApplyMapperAsync(
        Currency entity)
    {
        return CurrencyMapper.ToDto(entity);
    }

    public async Task<Result<CurrencyDto>> GetByCodeAsync(
        string code)
    {
        var currencies = await _currencyRepository
            .GetByCodeAsync(code);

        return await currencies.ToResultAsync(ApplyMapperAsync);
    }

    public async Task<Result<CurrencyDto>> GetBySymbolAsync(
        string symbol)
    {
        var currency = await _currencyRepository
            .GetBySymbolAsync(symbol);

        return await currency.ToResultAsync(ApplyMapperAsync);
    }
}