using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Caches.Keys.Baskets;
public class BasketItemCache
{
    private static readonly string Prefix =
        CacheKeys.LanguagePrefix<BasketItem>();

    public static string ByAdvertisementVariant(
        long variantId,
        string language,
        int page,
        int pageSize)
             => $"{Prefix}:{language}:advertisement-variant:{variantId}:page:{page}:size:{pageSize}";

    public static string ByAdvertisementVariantPattern(
        long variantId)
           => $"{Prefix}:*advertisement-variant:{variantId}:page:*";


    public static string ByBasket(
        long basketId,
        string language,
        int page,
        int pageSize)
             => $"{Prefix}:{language}:basket:{basketId}:page:{page}:size:{pageSize}";

    public static string ByBasketPattern(
        long basketId)
           => $"{Prefix}:*basket:{basketId}:page:*";
}