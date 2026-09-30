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

    public static async Task InvalidateAsync(
        ICacheService cache,
        Favorite favorite)
    {
        await cache.RemoveAsync(CacheKeys
            .Entity<Favorite>(favorite.Id));

        await cache.RemoveByPatternAsync(
            ByUserPattern(favorite.UserId));
    }
}