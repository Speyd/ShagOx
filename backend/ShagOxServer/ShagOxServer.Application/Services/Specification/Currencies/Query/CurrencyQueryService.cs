using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Specification.Currencies.Query;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Caches.Keys.Specification;
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
        ICurrencyQueryRepository currencyRepository,
        ICacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(currencyRepository, cacheService, settings)
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
        var cacheKey = CurrencyCache.ByCode(code);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var currencies = await _currencyRepository
                    .GetByCodeAsync(code);

                return await currencies.ToResultAsync(
                    ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }

    public async Task<Result<CurrencyDto>> GetBySymbolAsync(
        string symbol)
    {
        var cacheKey = CurrencyCache.BySymbol(symbol);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var currency = await _currencyRepository
                    .GetBySymbolAsync(symbol);

                return await currency.ToResultAsync(
                    ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }
}