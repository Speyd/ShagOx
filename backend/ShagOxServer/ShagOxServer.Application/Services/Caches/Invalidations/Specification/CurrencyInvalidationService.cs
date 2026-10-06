using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Application.DTOs.Specification.Currencies.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Specification;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Specification;
public class CurrencyInvalidationService
    : ICacheInvalidationService<Condition, CurrencyCacheInfo>
{
    private readonly IAdvertisementQueryRepository _advertRepository;
    private readonly AdvertisementInvalidationService _advertInvalid;

    private readonly ICacheService _cache;


    public CurrencyInvalidationService(
        IAdvertisementQueryRepository advertRepository,
        AdvertisementInvalidationService advertInvalid,
        ICacheService cache)
    {
        _advertRepository = advertRepository;
        _advertInvalid = advertInvalid;
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
       CurrencyCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
            SearchPattern<Currency>());
    }

    public async Task InvalidateDeleteAsync(
        CurrencyCacheInfo entityInfo)
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
        CurrencyCacheInfo entityInfo)
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
        CurrencyCacheInfo entityInfo)
    {
        await _cache.RemoveAsync(CacheKeys
            .Entity<Currency>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(CurrencyCache
           .ByCode(entityInfo.Code));

        await _cache.RemoveByPatternAsync(CurrencyCache
          .BySymbol(entityInfo.Symbol));

        await _cache.RemoveByPatternAsync(CacheKeys.
           EntityPagedPattern<Currency>());

        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<Currency>());
    }

    private async Task<List<AdvertisementCacheInfo>> GetAdvertisementInfos(
        long entityId)
    {
        return await _advertRepository
            .GetCacheInfosByConditionAsync(entityId);
    }
}