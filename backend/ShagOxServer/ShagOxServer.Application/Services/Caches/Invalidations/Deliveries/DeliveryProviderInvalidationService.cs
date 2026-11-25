using ShagOxServer.Application.Interfaces.Repositories.Deliveries.DeliveryProviders.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Deliveries.Translations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Cache.Deliveries;
using ShagOxServer.Domain.Cache.Translations;
using ShagOxServer.Domain.Entities.Deliveries;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Deliveries;
public class DeliveryProviderInvalidationService
    : ICacheInvalidationService<DeliveryProvider,
        DeliveryProviderCacheContext>
{
    private readonly IDeliveryProviderTranslationQueryRepository _transRepository;

    private readonly DeliveryProviderTranslationInvalidationService _transInvalid;

    private readonly IDataCacheService _cache;



    public DeliveryProviderInvalidationService(
        IDeliveryProviderTranslationQueryRepository transRepository,
        DeliveryProviderTranslationInvalidationService transInvalid,
        IDataCacheService cache)
    {
        _transRepository = transRepository;
        _transInvalid = transInvalid;
        _cache = cache;
    }

    public async Task InvalidateCreateAsync(
        DeliveryProviderCacheContext entityInfo)
    {
    }

    public async Task InvalidateDeleteAsync(
        DeliveryProviderCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);

        var translationInfos =
            await GetTranslationInfos(entityInfo.Id);

        foreach (var translationInfo in translationInfos)
        {
            await _transInvalid
                .InvalidateDeleteAsync(translationInfo);
        }
    }

    public async Task InvalidateUpdateAsync(
        DeliveryProviderCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
        DeliveryProviderCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
             EntityLanguagePattern<DeliveryProvider>(entityInfo.Id));

        await InvalidatePatternAsync(entityInfo);
    }

    private async Task InvalidatePatternAsync(
        DeliveryProviderCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePagedPattern<DeliveryProvider>(
                entityInfo.Status));

        await _cache.RemoveByPatternAsync(CacheKeys.
             LanguageSearchPattern<DeliveryProvider>(
                entityInfo.Status));
    }

    private async Task<List<TranslationCacheContext>> GetTranslationInfos(
        long entityId)
    {
        return await _transRepository
            .GetCacheInfosByTranslatableAsync(entityId);
    }
}
