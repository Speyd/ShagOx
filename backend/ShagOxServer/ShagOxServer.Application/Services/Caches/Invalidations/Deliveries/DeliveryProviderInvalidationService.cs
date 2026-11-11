using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Deliveries;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Deliveries;
public class DeliveryProviderInvalidationService
    : ICacheInvalidationService<DeliveryProvider, 
        DeliveryProviderCacheInfo>
{
    //private readonly IStatusTranslationQueryRepository _transRepository;

    //private readonly StatusTranslationInvalidationService _transInvalid;

    private readonly IDataCacheService _cache;



    public DeliveryProviderInvalidationService(
        //IStatusTranslationQueryRepository transRepository,
        //StatusTranslationInvalidationService transInvalid,
        IDataCacheService cache)
    {
       // _transRepository = transRepository;
        //_transInvalid = transInvalid;
        _cache = cache;
    }

    public async Task InvalidateCreateAsync(
        DeliveryProviderCacheInfo entityInfo)
    {
    }

    public async Task InvalidateDeleteAsync(
        DeliveryProviderCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);

        //var translationInfos =
        //    await GetTranslationInfos(entityInfo.Id);

        //foreach (var translationInfo in translationInfos)
        //{
        //    await _transInvalid
        //        .InvalidateDeleteAsync(translationInfo);
        //}
    }

    public async Task InvalidateUpdateAsync(
        DeliveryProviderCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
        DeliveryProviderCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
             EntityLanguagePattern<DeliveryProvider>(entityInfo.Id));

        await InvalidatePatternAsync(entityInfo);
    }

    private async Task InvalidatePatternAsync(
        DeliveryProviderCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePagedPattern<DeliveryProvider>(
                entityInfo.Status));

        await _cache.RemoveByPatternAsync(CacheKeys.
             LanguageSearchPattern<DeliveryProvider>(
                entityInfo.Status));
    }

    //private async Task<List<BaseTranslationCacheInfo>> GetTranslationInfos(
    //    long entityId)
    //{
    //    return await _transRepository
    //        .GetCacheInfosByTranslatableAsync(entityId);
    //}
}
