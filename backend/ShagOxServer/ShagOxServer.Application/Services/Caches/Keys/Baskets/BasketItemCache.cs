using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Caches.Keys.Baskets;
public static class BasketItemCache
{
    private static readonly string Prefix =
        CacheKeys.LanguagePrefix<BasketItem>();

    public static string ByAdvertisementVariant(
        long variantId,
        string language,
        int page,
        int pageSize,
        EntityStatus status = EntityStatus.Active)
             => $"{Prefix}:{language}:advertisement-variant:{variantId}:page:{page}:size:{pageSize}";

    public static string ByAdvertisementVariantPattern(
        long variantId,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:*status{status}:advertisement-variant:{variantId}:page:*";


    public static string ByBasket(
        long basketId,
        string language,
        int page,
        int pageSize,
        EntityStatus status = EntityStatus.Active)
             => $"{Prefix}:{language}:status{status}:basket:{basketId}:page:{page}:size:{pageSize}";

    public static string ByBasketPattern(
        long basketId,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:*status{status}:basket:{basketId}:page:*";
}
