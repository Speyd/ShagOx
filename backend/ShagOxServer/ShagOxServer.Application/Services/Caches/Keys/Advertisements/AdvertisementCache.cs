using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

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

    public static string BySellerPattern(
        long sellerId)
            => $"{Prefix}:*seller:{sellerId}:page:*";

    public static string ByBuyerPattern(
        long buyerId)
            => $"{Prefix}:*buyer:{buyerId}:page:*";

    public static string BySearch(
        AdvertisementSearchFilter filter,
        string language,
        int page,
        int pageSize)
    {
        var attributes = filter.Attributes
            .Order(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var data = new
        {
            filter.Title,
            filter.Description,
            filter.CategoryId,
            Attributes = attributes,
            language,
            page,
            pageSize
        };

        var json = JsonSerializer.Serialize(data);

        var hash = Convert.ToHexString(
            SHA256.HashData(
                Encoding.UTF8.GetBytes(json)));

        return $"{Prefix}:{language}:search:{hash}";
    }

    public static string SearchPattern()
        => $"{Prefix}:*:search:*";
}