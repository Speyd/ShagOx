using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Caches.Baskets;
public class BasketCache
{
    private static readonly string Prefix =
        CacheKeys.LanguagePrefix<Basket>();


    public static string ByUser(
       long userId,
       string language,
       int page,
       int pageSize)
            => $"{Prefix}:{language}:user:{userId}:page:{page}:size:{pageSize}";

    public static string ByUserPattern(
        long userId)
           => $"{Prefix}:*user:{userId}:page:*";
}