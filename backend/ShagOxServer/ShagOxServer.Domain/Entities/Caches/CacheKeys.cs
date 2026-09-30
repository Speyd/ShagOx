using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Domain.Entities.Caches;
public static class CacheKeys
{
    public static string Entity<T>(long id)
        => $"{typeof(T).Name.ToLowerInvariant()}:{id}";

    public static string Translation<T>(
        long id,
        string language)
        => $"{typeof(T).Name.ToLowerInvariant()}:{id}:translation:{language}";

    public static class Favorites
    {
        private static readonly string Prefix =
            typeof(Favorite).Name.ToLowerInvariant();

        public static string ByUser(
            long userId,
            int page,
            int pageSize)
            => $"{Prefix}:user:{userId}:page:{page}:size:{pageSize}";

        public static string ByUserPattern(long userId)
            => $"{Prefix}:user:{userId}:page:*";
    }

    public static class Advertisements
    {
        private static readonly string Prefix =
            typeof(Advertisement).Name.ToLowerInvariant();

        public static string BySeller(
            long sellerId,
            int page,
            int pageSize)
            => $"{Prefix}:seller:{sellerId}:page:{page}:size:{pageSize}";

        public static string BySellerPattern(long sellerId)
            => $"{Prefix}:seller:{sellerId}:page:*";

        public static string ByBuyer(
            long buyerId,
            int page,
            int pageSize)
            => $"{Prefix}:seller:{buyerId}:page:{page}:size:{pageSize}";

        public static string ByBuyerPattern(long buyerId)
            => $"{Prefix}:seller:{buyerId}:page:*";
    }
}