using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Caches.Keys.Baskets;
public static class BasketAttributeCache
{
    private static readonly string Prefix =
        CacheKeys.LanguagePrefix<BasketAttribute>();


    public static string ByAttributeDefenition(
        long attributeId,
        string language,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:{language}:status{status}:attribute-defenition:{attributeId}";

    public static string ByAttributeDefenitionPattern(
        long attributeId,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:*status{status}:attribute-defenition:{attributeId}";

    public static string ByCategory(
        long categoryId,
        string language,
        int page,
        int pageSize,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:{language}:status{status}:category:{categoryId}:page:{page}:size:{pageSize}";

    public static string ByCategoryPattern(
        long categoryId,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:*status{status}:category:{categoryId}:page:*";
}
