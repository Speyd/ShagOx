using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Caches.Keys.Baskets;
public class BasketCache
{
    private static readonly string Prefix =
        CacheKeys.LanguagePrefix<Basket>();


    public static string ByUser(
       long userId,
       string language)
            => $"{Prefix}:{language}:user:{userId}";

    public static string ByUserPattern(
        long userId)
           => $"{Prefix}:*user:{userId}";
}