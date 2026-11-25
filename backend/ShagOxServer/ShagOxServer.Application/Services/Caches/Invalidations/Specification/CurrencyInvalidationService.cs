using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Specification;
using ShagOxServer.Domain.Cache.Advertisements;
using ShagOxServer.Domain.Cache.Specification;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Specification;
public class CurrencyInvalidationService
    : ICacheInvalidationService<Currency, CurrencyCacheContext>
{
    private readonly IAdvertisementQueryRepository _advertRepository;
    private readonly AdvertisementInvalidationService _advertInvalid;

    private readonly IDataCacheService _cache;


    public CurrencyInvalidationService(
        IAdvertisementQueryRepository advertRepository,
        AdvertisementInvalidationService advertInvalid,
        IDataCacheService cache)
    {
        _advertRepository = advertRepository;
        _advertInvalid = advertInvalid;
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
       CurrencyCacheContext entityInfo)
    {
        await InvalidatePatternAsync(entityInfo);
    }

    public async Task InvalidateDeleteAsync(
        CurrencyCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);


        var advertInfos =
            await GetAdvertisementInfos(entityInfo.Id);

        foreach (var advertInfo in advertInfos)
        {
            await _advertInvalid
                .InvalidateDeleteAsync(advertInfo);
        }
    }

    public async Task InvalidateUpdateAsync(
        CurrencyCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);


        var advertInfos =
           await GetAdvertisementInfos(entityInfo.Id);

        foreach (var advertInfo in advertInfos)
        {
            await _advertInvalid
                .InvalidateUpdateAsync(advertInfo);
        }
    }

    private async Task InvalidateAsync(
        CurrencyCacheContext entityInfo)
    {
        await _cache.RemoveAsync(CacheKeys
            .Entity<Currency>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(CurrencyCache
           .ByCode(entityInfo.Code));

        await _cache.RemoveByPatternAsync(CurrencyCache
          .BySymbol(entityInfo.Symbol));

        await _cache.RemoveByPatternAsync(CurrencyCache
            .EntityByCodePattern(entityInfo.Code));

        await InvalidatePatternAsync(entityInfo);
    }

    private async Task InvalidatePatternAsync(
        CurrencyCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
           SearchPattern<Currency>(
               entityInfo.Status));

        await _cache.RemoveByPatternAsync(CacheKeys.
           EntityPagedPattern<Currency>(
                entityInfo.Status));
    }

    private async Task<List<AdvertisementCacheContext>> GetAdvertisementInfos(
        long entityId)
    {
        return await _advertRepository
            .GetCacheInfosByConditionAsync(entityId);
    }
}
