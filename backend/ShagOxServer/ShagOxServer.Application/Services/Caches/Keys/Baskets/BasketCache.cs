using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Caches.Keys.Baskets;
public static class BasketCache
{
    private static readonly string Prefix =
        CacheKeys.LanguagePrefix<Basket>();


    public static string ByUser(
       long userId,
       string language,
        EntityStatus status = EntityStatus.Active)
            => $"{Prefix}:{language}:status{status}:user:{userId}";

    public static string ByUserPattern(
        long userId,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:*status{status}:user:{userId}";
}
