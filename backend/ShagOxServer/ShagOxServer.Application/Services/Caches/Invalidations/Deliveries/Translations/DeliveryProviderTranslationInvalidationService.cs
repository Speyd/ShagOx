using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Deliveries.Localized;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Cache.Translations;
using ShagOxServer.Domain.Entities.Deliveries.Translations;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Deliveries.Translations;
public class DeliveryProviderTranslationInvalidationService
    : ITranslationCacheInvalidationService<DeliveryProviderTranslation>
{
    private readonly IDataCacheService _cache;
    private readonly DeliveryProviderLocalizedInvalidationService _delivery;

    public DeliveryProviderTranslationInvalidationService(
        DeliveryProviderLocalizedInvalidationService delivery,
        IDataCacheService cache)
    {
        _cache = cache;
        _delivery = delivery;
    }


    public async Task InvalidateCreateAsync(
       TranslationCacheContext cacheDto)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<DeliveryProviderTranslation>());

        await _cache.RemoveByPatternAsync(CacheKeys.
             EntityPagedPattern<DeliveryProviderTranslation>());
    }

    public async Task InvalidateDeleteAsync(
        TranslationCacheContext cacheDto)
    {
        await InvalidateAsync(cacheDto);
    }

    public async Task InvalidateUpdateAsync(
        TranslationCacheContext cacheDto)
    {
        await InvalidateAsync(cacheDto);
    }

    private async Task InvalidateAsync(
       TranslationCacheContext cacheDto)
    {
        await _cache.RemoveAsync(CacheKeys
            .Entity<DeliveryProviderTranslation>(cacheDto.Id));

        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityPagedPattern<DeliveryProviderTranslation>());

        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<DeliveryProviderTranslation>());

        await _delivery.InvalidateLanguageAsync(
            cacheDto.Language);
    }
}
