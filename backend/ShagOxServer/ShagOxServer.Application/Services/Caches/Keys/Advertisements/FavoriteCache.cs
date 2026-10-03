using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Domain.Caches.Advertisements;
public class FavoriteCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<Favorite>();

    public static string ByUser(
        long userId,
        int page,
        int pageSize)
            => $"{Prefix}:user:{userId}:page:{page}:size:{pageSize}";

    public static string ByUserPattern(
        long userId)
            => $"{Prefix}:user:{userId}:page:*";
}