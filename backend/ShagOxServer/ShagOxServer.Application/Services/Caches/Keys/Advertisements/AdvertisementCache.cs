using ShagOxServer.Application.Common.Helpers;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Services.Caches.Advertisements;
public static class AdvertisementCache
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

    public static string ByShortPaged(
        string language,
        int page,
        int pageSize)
            => $"{Prefix}:{language}:page:{page}:size:{pageSize}";

    public static string BySellerPattern(
        long sellerId)
            => $"{Prefix}:*seller:{sellerId}:page:*";

    public static string ByBuyerPattern(
        long buyerId)
            => $"{Prefix}:*buyer:{buyerId}:page:*";

    public static string ByShortSearch(
        AdvertisementSearchFilter filter,
        string language,
        int page,
        int pageSize)
    {
        var hash = CacheKeyHelper.GetSearchHash(
            filter,
            page,
            pageSize);

        return $"{Prefix}:{language}:search:{hash}";
    }

}