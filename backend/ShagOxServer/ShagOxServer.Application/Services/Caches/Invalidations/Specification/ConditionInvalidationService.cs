using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Specification;
public class ConditionInvalidationService
    : ICacheInvalidationService<Condition, long>
{

    private readonly IAdvertisementQueryRepository _advertRepository;
    private readonly AdvertisementInvalidationService _advertInvalid;

    //private readonly IRegionTranslationQueryRepository _transRepository;
    //private readonly RegionTranslationInvalidationService _transInvalid;

    private readonly ICacheService _cache;



    public ConditionInvalidationService(
        IAdvertisementQueryRepository advertRepository,
        AdvertisementInvalidationService advertInvalid,
        //IRegionTranslationQueryRepository transRepository,
        //RegionTranslationInvalidationService transInvalid,
        ICacheService cache)
    {
        _advertRepository = advertRepository;
        _advertInvalid = advertInvalid;
        //_transRepository = transRepository;
        //_transInvalid = transInvalid;
        _cache = cache;
    }


    public async Task InvalidateDeleteAsync(
        long entityId)
    {
        await InvalidateAsync(entityId);


        var advertInfos =
            await GetAdvertisementInfos(entityId);

        foreach (var advertInfo in advertInfos)
        {
            await _advertInvalid
                .InvalidateDeleteAsync(advertInfo);
        }

        //var transInfos =
        //   await GetTranslationInfos(entityId);

        //foreach (var transInfo in transInfos)
        //{
        //    await _transInvalid
        //        .InvalidateDeleteAsync(transInfo);
        //}
    }

    public async Task InvalidateUpdateAsync(
        long entityId)
    {
        await InvalidateAsync(entityId);


        var advertInfos =
           await GetAdvertisementInfos(entityId);

        foreach (var advertInfo in advertInfos)
        {
            await _advertInvalid
                .InvalidateUpdateAsync(advertInfo);
        }
    }

    private async Task InvalidateAsync(
        long entityId)
    {
        await _cache.RemoveByPatternAsync(CacheKeys
            .EntityLanguagePattern<Condition>(entityId));
    }

    private async Task<List<AdvertisementCacheInfo>> GetAdvertisementInfos(
        long entityId)
    {
        return await _advertRepository
            .GetCacheInfosByConditionAsync(entityId);
    }

    //private async Task<List<BaseTranslationCacheInfo>> GetTranslationInfos(
    //    long entityId)
    //{
    //    return await _transRepository
    //        .GetCacheInfosByTranslatableAsync(entityId);
    //}
}