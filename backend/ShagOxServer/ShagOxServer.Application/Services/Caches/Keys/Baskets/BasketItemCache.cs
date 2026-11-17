using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Caches.Keys.Baskets;
public static class BasketItemCache
{
    private static readonly string Prefix =
        CacheKeys.LanguagePrefix<BasketItem>();


    public static string ByBasket(
        long basketId,
        string language,
        int page,
        int pageSize,
        EntityStatus status = EntityStatus.Active)
             => $"{Prefix}:{language}:status:{status}:basket:{basketId}:page:{page}:size:{pageSize}";

    public static string ByBasketPattern(
        long basketId,
        EntityStatus status)
           => $"{Prefix}:*:status:{status}:basket:{basketId}:page:*";

    public static string ByBasketPattern(
        long basketId)
           => $"{Prefix}:*:basket:{basketId}:page:*";
}
