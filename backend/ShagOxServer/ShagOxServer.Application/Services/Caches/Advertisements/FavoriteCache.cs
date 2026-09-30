using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Domain.Caches.Advertisements;
public class FavoriteCache
{
    private static readonly string Prefix =
           typeof(Favorite).Name.ToLowerInvariant();

    public static string ByUser(
        long userId,
        int page,
        int pageSize)
        => $"{Prefix}:user:{userId}:page:{page}:size:{pageSize}";

    public static string ByUserPattern(long userId)
        => $"{Prefix}:user:{userId}:page:*";

    public static async Task InvalidateCreateAsync(
        ICacheService cache,
        Favorite cachedEntity)
    {
        await cache.RemoveByPatternAsync(
            ByUserPattern(cachedEntity.UserId));
    }

    public static async Task InvalidateDeleteAsync(
        ICacheService cache,
        Favorite cachedEntity)
    {
        await cache.RemoveAsync(CacheKeys
            .Entity<Favorite>(cachedEntity.Id));

        await cache.RemoveByPatternAsync(
            ByUserPattern(cachedEntity.UserId));
    }

    public static async Task InvalidateUpdateAsync(
        ICacheService cache,
        Favorite cachedEntity)
    {
        await cache.RemoveAsync(CacheKeys
            .Entity<Favorite>(cachedEntity.Id));

        await cache.RemoveByPatternAsync(
            ByUserPattern(cachedEntity.UserId));
    }
}