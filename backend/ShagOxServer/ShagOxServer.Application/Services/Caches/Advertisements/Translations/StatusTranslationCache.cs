using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements.Translations;

namespace ShagOxServer.Application.Services.Caches.Advertisements.Translations;
public class StatusTranslationCache
{
    private static readonly string Prefix =
           typeof(StatusTranslation).Name.ToLowerInvariant();

    public static async Task InvalidateCreateAsync(
        ICacheService cache,
        StatusTranslation cachedEntity)
    {
        await cache.RemoveAsync(
            CacheKeys.Translation<Status>(
                cachedEntity.Translatable.Id,
                cachedEntity.Language));
    }

    public static async Task InvalidateDeleteAsync(
        ICacheService cache,
        StatusTranslation cachedEntity)
    {
        await cache.RemoveAsync(CacheKeys
            .Entity<StatusTranslation>(cachedEntity.Id));

        await cache.RemoveAsync(
            CacheKeys.Translation<Status>(
                cachedEntity.Translatable.Id,
                cachedEntity.Language));
    }

    public static async Task InvalidateUpdateAsync(
        ICacheService cache,
        StatusTranslation cachedEntity)
    {
        await cache.RemoveAsync(CacheKeys
            .Entity<StatusTranslation>(cachedEntity.Id));

        await cache.RemoveAsync(
            CacheKeys.Translation<Status>(
                cachedEntity.Translatable.Id,
                cachedEntity.Language));
    }
}