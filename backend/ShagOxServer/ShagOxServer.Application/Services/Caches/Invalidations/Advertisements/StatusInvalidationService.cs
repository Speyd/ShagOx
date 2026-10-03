using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements.Translations;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
public class StatusInvalidationService
    : ICacheInvalidationService<Status, long>
{
    private readonly IStatusTranslationQueryRepository _transRepository;

    private readonly StatusTranslationInvalidationService _transInvalid;

    private readonly IAdvertisementQueryRepository _advertRepository;

    private readonly AdvertisementInvalidationService _advertInvalid;

    private readonly ICacheService _cache;



    public StatusInvalidationService(
        IStatusTranslationQueryRepository transRepository,
        StatusTranslationInvalidationService transInvalid,
        IAdvertisementQueryRepository advertRepository,
        AdvertisementInvalidationService advertInvalid,
        ICacheService cache)
    {
        _transRepository = transRepository;
        _transInvalid = transInvalid;
        _advertRepository = advertRepository;
        _advertInvalid = advertInvalid;
        _cache = cache;
    }


    public async Task InvalidateDeleteAsync(
        long entityId)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
             EntityLanguagePattern<Status>(entityId));

        var translationInfos = 
            await GetTranslationInfos(entityId);
   
        foreach (var translationInfo in translationInfos)
        {
            await _transInvalid
                .InvalidateDeleteAsync(translationInfo);
        }

        if (translationInfos.Count == 0)
        {
            var advertInfos =
                await GetAdvertisementInfos(entityId);

            foreach (var advertInfo in advertInfos)
            {
                await _advertInvalid
                    .InvalidateDeleteAsync(advertInfo);
            }
        }
    }

    public async Task InvalidateUpdateAsync(
        long entityId)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
             EntityLanguagePattern<Status>(entityId));

        var translationInfos =
            await GetTranslationInfos(entityId);

        foreach (var translationInfo in translationInfos)
        {
            await _transInvalid
                .InvalidateUpdateAsync(translationInfo);
        }

        if (translationInfos.Count == 0)
        {
            var advertInfos =
                await GetAdvertisementInfos(entityId);

            foreach (var advertInfo in advertInfos)
            {
                await _advertInvalid
                    .InvalidateUpdateAsync(advertInfo);
            }
        }
    }

    private async Task<List<BaseTranslationCacheInfo>> GetTranslationInfos(
        long entityId)
    {
        return await _transRepository
            .GetCacheInfoByTranslatableAsync(entityId);
    }

    private async Task<List<AdvertisementCacheInfo>> GetAdvertisementInfos(
        long entityId)
    {
        return await _advertRepository
            .GetCacheInfoByStatusAsync(entityId);
    }
}