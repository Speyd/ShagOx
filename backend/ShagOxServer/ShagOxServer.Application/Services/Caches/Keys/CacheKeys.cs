using ShagOxServer.Application.Common.Helpers;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using System.Net.NetworkInformation;

namespace ShagOxServer.Application.Services.Caches.Keys;
public static class CacheKeys
{
    #region Prefixes

    public static string Prefix<TEntity>()
        => $"{typeof(TEntity).Name.ToLowerInvariant()}";

    public static string LanguagePrefix<TEntity>()
        => $"{Prefix<TEntity>()}:language";

    public static string LanguagePrefix(string prefix)
        => $"{prefix}:language";

    #endregion


    #region Entity

    public static string Entity<TEntity>(
        long id,
        EntityStatus status = EntityStatus.Active)
        => $"{Prefix<TEntity>()}:{id}:status:{status}";

    public static string EntityPattern<TEntity>(
        EntityStatus status = EntityStatus.Active)
        => $"{Prefix<TEntity>()}:*status:{status}";

    #endregion


    #region Paged

    public static string EntityPaged<TEntity>(
        int page,
        int pageSize,
        EntityStatus status = EntityStatus.Active)
        => $"{Prefix<TEntity>()}:status:{status}:paged:page:{page}:size:{pageSize}";

    public static string EntityPagedPattern<TEntity>(
        EntityStatus status = EntityStatus.Active)
        => $"{Prefix<TEntity>()}:status:{status}:paged:*";

    #endregion


    #region Language Entity

    public static string EntityLanguage<TEntity>(
        long id,
        string language,
        EntityStatus status = EntityStatus.Active)
        => $"{LanguagePrefix<TEntity>()}:{language}:status:{status}:entity:{id}";

    public static string EntityLanguagePattern<TEntity>(
        long id,
        EntityStatus status = EntityStatus.Active)
        => $"{LanguagePrefix<TEntity>()}:*:status:{status}:entity:{id}";

    public static string EntityLanguagePattern<TEntity>(
        string language,
        EntityStatus status)
        => $"{LanguagePrefix<TEntity>()}:{language}:status:{status}:entity:*";

    public static string EntityLanguagePattern<TEntity>(
        string language)
        => $"{LanguagePrefix<TEntity>()}:{language}:*";

    #endregion


    #region Language

    public static string Language<TEntity>(
        string language,
        EntityStatus status)
        => $"{LanguagePrefix<TEntity>()}:{language}:status:{status}";

    public static string LanguagePattern<TEntity>(
        EntityStatus status = EntityStatus.Active)
        => $"{LanguagePrefix<TEntity>()}:*:status:{status}";

    public static string LanguagePattern<TEntity>(
        string language)
        => $"{LanguagePrefix<TEntity>()}:{language}:*";

    #endregion


    #region Language Paged

    public static string EntityLanguagePaged<TEntity>(
        string language,
        int page,
        int pageSize,
        EntityStatus status = EntityStatus.Active)
        => $"{LanguagePrefix<TEntity>()}:{language}:status:{status}:paged:page:{page}:size:{pageSize}";

    public static string EntityLanguagePagedPattern<TEntity>(
        EntityStatus status = EntityStatus.Active)
        => $"{LanguagePrefix<TEntity>()}:*:status:{status}:paged:page:*";

    public static string EntityLanguagePagedPattern<TEntity>(
        string language,
        EntityStatus status)
        => $"{LanguagePrefix<TEntity>()}:{language}:status:{status}:paged:*";

    public static string EntityLanguagePagedPattern<TEntity>(
        string language)
        => $"{LanguagePrefix<TEntity>()}:{language}:*";

    #endregion


    #region Search

    private static string BuildSearchKey<TEntity, TFilter>(
        TFilter filter,
        int page,
        int pageSize,
        string? language = null,
        EntityStatus status = EntityStatus.Active)
    {
        var hash = CacheKeyHelper.GetSearchHash(
            filter,
            page,
            pageSize);

        return language is null
            ? $"{Prefix<TEntity>()}:status:{status}:search:{hash}"
            : $"{LanguagePrefix<TEntity>()}:{language}:status:{status}:search:{hash}";
    }

    public static string BySearch<TEntity, TFilter>(
        TFilter filter,
        int page,
        int pageSize,
        EntityStatus status = EntityStatus.Active)
    {
        return BuildSearchKey<TEntity, TFilter>(
            filter,
            page,
            pageSize);
    }

    public static string SearchPattern<TEntity>(
        EntityStatus status = EntityStatus.Active)
        => $"{Prefix<TEntity>()}:status:{EntityStatus.Active}:search:*";

    #endregion


    #region Language Search

    public static string ByLanguageSearch<TEntity, TFilter>(
        TFilter filter,
        string language,
        int page,
        int pageSize,
        EntityStatus status = EntityStatus.Active)
    {
        return BuildSearchKey<TEntity, TFilter>(
            filter,
            page,
            pageSize,
            language,
            status);
    }

    public static string LanguageSearchPattern<TEntity>(
        EntityStatus status = EntityStatus.Active)
        => $"{LanguagePrefix<TEntity>()}:status:{status}:*:search:*";

    public static string LanguageSearchPattern<TEntity>(
        string language,
        EntityStatus status)
        => $"{LanguagePrefix<TEntity>()}:{language}:status:{status}:search:*";

    public static string LanguageSearchPattern<TEntity>(
        string language)
        => $"{LanguagePrefix<TEntity>()}:{language}:status:*:search:*";

    #endregion
}
