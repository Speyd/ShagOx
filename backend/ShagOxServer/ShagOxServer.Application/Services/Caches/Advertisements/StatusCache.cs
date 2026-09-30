using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Advertisements;
using System.Globalization;

namespace ShagOxServer.Application.Services.Caches.Advertisements;
public class StatusCache
{
    private static readonly string Prefix =
           typeof(Status).Name.ToLowerInvariant();


    public static async Task InvalidateDeleteAsync(
        ICacheService cache,
        Status cachedEntity)
    {
        await cache.RemoveAsync(
                CacheKeys.Entity<Status>(cachedEntity.Id));

        await cache.RemoveAsync(
            CacheKeys.Translation<Status>(
                cachedEntity.Id,
                CultureInfo.CurrentCulture.Name));
    }

    public static async Task InvalidateUpdateAsync(
        ICacheService cache,
        Status cachedEntity)
    {
        await cache.RemoveAsync(
                CacheKeys.Entity<Status>(cachedEntity.Id));

        await cache.RemoveAsync(
            CacheKeys.Translation<Status>(
                cachedEntity.Id,
                CultureInfo.CurrentCulture.Name));
    }
}