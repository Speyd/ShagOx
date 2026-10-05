using ShagOxServer.Application.Common.Helpers;

namespace ShagOxServer.Application.Services.Caches.Keys;
public static class CacheKeys
{
    public static string Prefix<TEntity>()
        => $"{typeof(TEntity).Name.ToLowerInvariant()}";

    public static string LanguagePrefix<TEntity>()
        => $"{Prefix<TEntity>()}:language";


    public static string Entity<TEntity>(
        long id)
        => $"{Prefix<TEntity>()}:{id}";
    public static string EntityPattern<TEntity>()
        => $"{Prefix<TEntity>()}:*";

    public static string EntityPaged<TEntity>(
        int page,
        int pageSize)
        => $"{Prefix<TEntity>()}:page:{page}:size:{pageSize}";


    public static string EntityLanguage<TEntity>(
        long id,
        string language)
        => $"{LanguagePrefix<TEntity>()}:{language}:entity:{id}";

    public static string EntityLanguagePaged<TEntity>(
        string language,
        int page,
        int pageSize)
        => $"{LanguagePrefix<TEntity>()}:{language}:page:{page}:size:{pageSize}";

    public static string EntityLanguagePattern<TEntity>(
        long id)
     => $"{LanguagePrefix<TEntity>()}:*:entity:{id}";

    public static string EntityLanguagePattern<TEntity>(
        string language)
    => $"{LanguagePrefix<TEntity>()}:{language}:entity:*";



    public static string Language<TEntity>(
        string language)
    => $"{LanguagePrefix<TEntity>()}:{language}";

    public static string LanguagePattern<TEntity>()
       => $"{LanguagePrefix<TEntity>()}:*";

    public static string LanguagePattern<TEntity>(
        string language)
    => $"{LanguagePrefix<TEntity>()}:{language}*";


    private static string BuildSearchKey<TEntity, TFilter>(
        TFilter filter,
        int page,
        int pageSize,
        string? language = null)
    {
        var hash = CacheKeyHelper.GetSearchHash(
            filter,
            page,
            pageSize);

        return language is null
            ? $"{Prefix<TEntity>()}:search:{hash}"
            : $"{LanguagePrefix<TEntity>()}:{language}:search:{hash}";
    }

    public static string BySearch<TEntity, TFilter>(
        TFilter filter,
        int page,
        int pageSize)
    {
        return BuildSearchKey<TEntity, TFilter>(
            filter,
            page,
            pageSize);
    }

    public static string SearchPattern<TEntity>()
        => $"{Prefix<TEntity>()}:search:*";
    
    public static string ByLanguageSearch<TEntity, TFilter>(
        TFilter filter,
        string language,
        int page,
        int pageSize)
    {
        return BuildSearchKey<TEntity, TFilter>(
            filter,
            page,
            pageSize,
            language);
    }

    public static string LanguageSearchPattern<TEntity>()
       => $"{LanguagePrefix<TEntity>()}:*:search:*";
}