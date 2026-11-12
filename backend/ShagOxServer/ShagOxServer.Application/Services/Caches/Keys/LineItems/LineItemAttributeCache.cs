using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.LineItems;

namespace ShagOxServer.Application.Services.Caches.Keys.LineItems;
public static class LineItemAttributeCache
{
    private static readonly string Prefix =
        CacheKeys.LanguagePrefix<LineItemAttribute>();


    public static string ByAttributeDefenition(
        long attributeId,
        string language,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:{language}:status:{status}:attribute-defenition:{attributeId}";

    public static string ByAttributeDefenitionPattern(
        long attributeId,
        EntityStatus status)
           => $"{Prefix}:*:status:{status}:attribute-defenition:{attributeId}";

    public static string ByAttributeDefenitionPattern(
        long attributeId)
           => $"{Prefix}:*:attribute-defenition:{attributeId}";

    public static string ByCategory(
        long categoryId,
        string language,
        int page,
        int pageSize,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:{language}:status:{status}:category:{categoryId}:page:{page}:size:{pageSize}";

    public static string ByCategoryPattern(
        long categoryId,
        EntityStatus status)
           => $"{Prefix}:*:status:{status}:category:{categoryId}:page:*";

    public static string ByCategoryPattern(
        long categoryId)
           => $"{Prefix}:*:category:{categoryId}:page:*";
}
