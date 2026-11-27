using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Domain.Caches.Advertisements;
public static class FavoriteCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<Favorite>();

    public static string ByUser(
        long userId,
        int page,
        int pageSize,
        string currency,
        EntityStatus status = EntityStatus.Active)
            => $"{Prefix}:status:{status}:user:{userId}:page:{page}:size:{pageSize}:{currency}";

    public static string ByUserPattern(
        long userId,
        EntityStatus status)
            => $"{Prefix}:status:{status}:user:{userId}:page:*";

    public static string ByUserPattern(
        long userId)
            => $"{Prefix}:*:user:{userId}:page:*";
}
