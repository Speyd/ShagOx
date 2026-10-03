namespace ShagOxServer.Domain.Caches;
public static class CacheKeys
{
    public static string Prefix<T>()
        => $"{typeof(T).Name.ToLowerInvariant()}";

    public static string LanguagePrefix<T>()
        => $"{Prefix<T>()}:language";


    public static string Entity<T>(long id)
        => $"{Prefix<T>()}:{id}";
    public static string EntityPattern<T>()
        => $"{Prefix<T>()}:*";


    public static string EntityLanguage<T>(
        long id,
        string language)
        => $"{LanguagePrefix<T>()}:{language}:entity:{id}";

    public static string EntityLanguagePattern<T>(
        long id)
     => $"{LanguagePrefix<T>()}:*:entity:{id}";

    public static string EntityLanguagePattern<T>(
        string language)
    => $"{LanguagePrefix<T>()}:{language}:entity:*";


    public static string Language<T>(string language)
        => $"{LanguagePrefix<T>()}:{language}";

    public static string LanguagePattern<T>()
       => $"{LanguagePrefix<T>()}:*";

    public static string LanguagePattern<T>(string language)
       => $"{LanguagePrefix<T>()}:{language}*";
}