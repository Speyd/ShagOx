using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.DTOs.Specification.Conditions.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Translations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Specification;
public class ConditionInvalidationService
    : ICacheInvalidationService<Condition, ConditionCacheInfo>
{

    private readonly IAdvertisementQueryRepository _advertRepository;
    private readonly AdvertisementInvalidationService _advertInvalid;

    private readonly IConditionTranslationQueryRepository _transRepository;
    private readonly ConditionTranslationInvalidationService _transInvalid;

    private readonly IDataCacheService _cache;



    public ConditionInvalidationService(
        IAdvertisementQueryRepository advertRepository,
        AdvertisementInvalidationService advertInvalid,
        IConditionTranslationQueryRepository transRepository,
        ConditionTranslationInvalidationService transInvalid,
        IDataCacheService cache)
    {
        _advertRepository = advertRepository;
        _advertInvalid = advertInvalid;
        _transRepository = transRepository;
        _transInvalid = transInvalid;
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
       ConditionCacheInfo entityInfo)
    {
        await InvalidatePatternAsync(entityInfo);
    }

    public async Task InvalidateDeleteAsync(
        ConditionCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);


        var advertInfos =
            await GetAdvertisementInfos(entityInfo.Id);

        foreach (var advertInfo in advertInfos)
        {
            await _advertInvalid
                .InvalidateDeleteAsync(advertInfo);
        }

        var transInfos =
           await GetTranslationInfos(entityInfo.Id);

        foreach (var transInfo in transInfos)
        {
            await _transInvalid
                .InvalidateDeleteAsync(transInfo);
        }
    }

    public async Task InvalidateUpdateAsync(
        ConditionCacheInfo entityInfo)
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
        ConditionCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys
            .EntityLanguagePattern<Condition>(entityInfo.Id));

        await InvalidatePatternAsync(entityInfo);
    }

    private async Task InvalidatePatternAsync(
        ConditionCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
           EntityLanguagePagedPattern<Condition>(
                entityInfo.Status));

        await _cache.RemoveByPatternAsync(CacheKeys.
             LanguageSearchPattern<Condition>(
                entityInfo.Status));
    }

    private async Task<List<AdvertisementCacheInfo>> GetAdvertisementInfos(
        long entityId)
    {
        return await _advertRepository
            .GetCacheInfosByConditionAsync(entityId);
    }

    private async Task<List<BaseTranslationCacheInfo>> GetTranslationInfos(
        long entityId)
    {
        return await _transRepository
            .GetCacheInfosByTranslatableAsync(entityId);
    }
}
