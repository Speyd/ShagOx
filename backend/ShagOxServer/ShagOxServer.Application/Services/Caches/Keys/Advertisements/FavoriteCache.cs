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
        EntityStatus status = EntityStatus.Active)
            => $"{Prefix}:status{status}:user:{userId}:page:{page}:size:{pageSize}";

    public static string ByUserPattern(
        long userId,
        EntityStatus status = EntityStatus.Active)
            => $"{Prefix}:status{status}:user:{userId}:page:*";
}
