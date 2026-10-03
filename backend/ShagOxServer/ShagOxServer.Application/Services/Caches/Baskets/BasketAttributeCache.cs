using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Caches.Baskets;
public class BasketAttributeCache
{
    private static readonly string Prefix =
        CacheKeys.LanguagePrefix<BasketAttribute>();


    public static string ByAttributeDefenition(
        long attributeId,
        string language)
           => $"{Prefix}:{language}:attribute-defenition:{attributeId}";

    public static string ByAttributeDefenitionPattern(
        long attributeId)
           => $"{Prefix}:*attribute-defenition:{attributeId}";

    public static string ByCategory(
        long categoryId,
        string language,
        int page,
        int pageSize)
           => $"{Prefix}:{language}:category:{categoryId}:page:{page}:size:{pageSize}";

    public static string ByCategoryPattern(
        long categoryId)
           => $"{Prefix}:*category:{categoryId}:page:*";
}