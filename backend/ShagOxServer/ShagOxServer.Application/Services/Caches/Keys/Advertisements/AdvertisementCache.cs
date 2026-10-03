using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Caches.Advertisements;
public class AdvertisementCache
{
    public static readonly string Prefix =
        CacheKeys.LanguagePrefix<Advertisement>();


    public static string BySeller(
        long sellerId,
        string language,
        int page,
        int pageSize)
            => $"{Prefix}:{language}:seller:{sellerId}:page:{page}:size:{pageSize}";

    public static string ByBuyer(
        long buyerId,
        string language,
        int page,
        int pageSize)
            => $"{Prefix}:{language}:buyer:{buyerId}:page:{page}:size:{pageSize}";

    public static string BySellerPattern(
        long sellerId)
            => $"{Prefix}:*seller:{sellerId}:page:*";

    public static string ByBuyerPattern(
        long buyerId)
            => $"{Prefix}:*buyer:{buyerId}:page:*";
}