using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Caches;
using ShagOxServer.Application.DTOs.Specification.Currencies.Query;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Query;
using ShagOxServer.Application.Services.Base.Query;
using ShagOxServer.Application.Services.Caches.Keys.Specification;
using ShagOxServer.Application.Services.Specification.Currencies.Mapping;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth;
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
        IDataCacheService cacheService,
        IOptions<CacheOptions> cacheOptions
    )
        : base(currencyRepository, cacheService, cacheOptions)
    {
        _currencyRepository = currencyRepository;
    }


    public override async Task<CurrencyDto> ApplyMapperAsync(
        Currency entity)
    {
        return CurrencyMapper.ToDto(entity);
    }

    public async Task<Result<CurrencyDto>> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = CurrencyCache.ByCode(
            code,
            status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var currencies = await _currencyRepository
                    .GetByCodeAsync(code, cancellationToken);

                return await currencies.ToResultAsync(
                    ApplyMapperAsync);
            },
            _cacheOptions.DefaultExpiration
        );
    }

    public async Task<Result<CurrencyDto>> GetBySymbolAsync(
        string symbol,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = CurrencyCache.BySymbol(
            symbol,
            status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var currency = await _currencyRepository
                    .GetBySymbolAsync(symbol, 
                        cancellationToken, status);

                return await currency.ToResultAsync(
                    ApplyMapperAsync);
            },
            _cacheOptions.DefaultExpiration
        );
    }

    public async Task<Result<Currency?>> GetByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = CurrencyCache.ByUser(
            userId,
            status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var currency = await _currencyRepository
                    .GetByUserAsync(userId,
                        cancellationToken, status);

                return Result<Currency?>.Success(currency);
            },
            _cacheOptions.DefaultExpiration
        );
    }

    public async Task<Result<Currency?>> GetEntityByCodeAsync(
        string code, 
        CancellationToken cancellationToken = default, 
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = CurrencyCache.EntityByCode(
            code,
            status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var currency = await _currencyRepository
                    .GetByCodeAsync(code,
                        cancellationToken, status);

                return Result<Currency?>.Success(currency);
            },
            _cacheOptions.DefaultExpiration
        );
    }
}
