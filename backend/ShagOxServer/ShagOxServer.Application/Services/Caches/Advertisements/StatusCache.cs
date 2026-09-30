using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements.Translations;
using System.Globalization;

namespace ShagOxServer.Application.Services.Caches.Advertisements;
public class StatusCache
{
    private static readonly string Prefix =
           typeof(Status).Name.ToLowerInvariant();


    public static async Task InvalidateDeleteAsync(
        ICacheService cache,
        IStatusTranslationQueryRepository repository,
        Status cachedEntity)
    {
        await cache.RemoveByPatternAsync(
            CacheKeys.Entity<Status>(cachedEntity.Id));

        var translationIds = await repository
            .GetTranslationIdsByTranslatableAsync(cachedEntity.Id);

        foreach (var translationId in translationIds)
        {
            await cache.RemoveAsync(
                CacheKeys.Entity<StatusTranslation>(translationId));
        }
    }

    public static async Task InvalidateUpdateAsync(
        ICacheService cache,
        Status cachedEntity)
    {
        await cache.RemoveByPatternAsync(
             CacheKeys.Entity<Status>(cachedEntity.Id));
    }

    public static async Task InvalidateByTranslationAsync(
        ICacheService cache,
        StatusTranslation translation,
        Status cachedEntity)
    {
        await cache.RemoveAsync(
            CacheKeys.Translation<Status>(
                cachedEntity.Id,
                translation.Language));
    }
}