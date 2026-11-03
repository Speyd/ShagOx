using ShagOxServer.Application.Common.Helpers;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Base.Enum;
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
        int pageSize,
        EntityStatus status = EntityStatus.Active)
            => $"{Prefix}:{language}:status{status}:seller:{sellerId}:page:{page}:size:{pageSize}";

    public static string ByBuyer(
        long buyerId,
        string language,
        int page,
        int pageSize,
        EntityStatus status = EntityStatus.Active)
            => $"{Prefix}:{language}:status{status}:buyer:{buyerId}:page:{page}:size:{pageSize}";

    public static string ByShortPaged(
        string language,
        int page,
        int pageSize,
        EntityStatus status = EntityStatus.Active)
            => $"{Prefix}:{language}:status{status}:page:{page}:size:{pageSize}";

    public static string BySellerPattern(
        long sellerId,
        EntityStatus status = EntityStatus.Active)
            => $"{Prefix}:*:status{status}:seller:{sellerId}:page:*";

    public static string ByBuyerPattern(
        long buyerId,
        EntityStatus status = EntityStatus.Active)
            => $"{Prefix}:*:status{status}:buyer:{buyerId}:page:*";

    public static string ByShortSearch(
        AdvertisementSearchFilter filter,
        string language,
        int page,
        int pageSize,
        EntityStatus status = EntityStatus.Active)
    {
        var hash = CacheKeyHelper.GetSearchHash(
            filter,
            page,
            pageSize);

        return $"{Prefix}:{language}:status{status}:search:{hash}";
    }

}
