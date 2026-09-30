namespace ShagOxServer.Domain.Caches;
public static class CacheKeys
{
    public static string Entity<T>(long id)
        => $"{typeof(T).Name.ToLowerInvariant()}:{id}";

    public static string Translation<T>(
        long id,
        string language)
        => $"{typeof(T).Name.ToLowerInvariant()}:{id}:translation:{language}";
}