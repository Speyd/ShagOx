using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Cache;
using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements.Translations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
public class StatusInvalidationService
    : ICacheInvalidationService<Status, StatusCacheInfo>
{
    private readonly IStatusTranslationQueryRepository _transRepository;

    private readonly StatusTranslationInvalidationService _transInvalid;

    private readonly IAdvertisementQueryRepository _advertRepository;

    private readonly AdvertisementInvalidationService _advertInvalid;

    private readonly IDataCacheService _cache;



    public StatusInvalidationService(
        IStatusTranslationQueryRepository transRepository,
        StatusTranslationInvalidationService transInvalid,
        IAdvertisementQueryRepository advertRepository,
        AdvertisementInvalidationService advertInvalid,
        IDataCacheService cache)
    {
        _transRepository = transRepository;
        _transInvalid = transInvalid;
        _advertRepository = advertRepository;
        _advertInvalid = advertInvalid;
        _cache = cache;
    }

    public async Task InvalidateCreateAsync(
        StatusCacheInfo entityInfo)
    {
        await InvalidatePatternAsync(entityInfo);
    }

    public async Task InvalidateDeleteAsync(
        StatusCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);

        var translationInfos = 
            await GetTranslationInfos(entityInfo.Id);
   
        foreach (var translationInfo in translationInfos)
        {
            await _transInvalid
                .InvalidateDeleteAsync(translationInfo);
        }

        var advertInfos =
             await GetAdvertisementInfos(entityInfo.Id);

        foreach (var advertInfo in advertInfos)
        {
            await _advertInvalid
                .InvalidateDeleteAsync(advertInfo);
        }
    }

    public async Task InvalidateUpdateAsync(
        StatusCacheInfo entityInfo)
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
        StatusCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
             EntityLanguagePattern<Status>(entityInfo.Id));

        await InvalidatePatternAsync(entityInfo);
    }

    private async Task InvalidatePatternAsync(
        StatusCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePagedPattern<Status>(
                entityInfo.Status));

        await _cache.RemoveByPatternAsync(CacheKeys.
             LanguageSearchPattern<Status>(
                entityInfo.Status));
    }

    private async Task<List<BaseTranslationCacheInfo>> GetTranslationInfos(
        long entityId)
    {
        return await _transRepository
            .GetCacheInfosByTranslatableAsync(entityId);
    }

    private async Task<List<AdvertisementCacheInfo>> GetAdvertisementInfos(
        long entityId)
    {
        return await _advertRepository
            .GetCacheInfosByStatusAsync(entityId);
    }
}
