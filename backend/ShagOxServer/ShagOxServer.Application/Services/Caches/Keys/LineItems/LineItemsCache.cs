using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Base.Items;

namespace ShagOxServer.Application.Services.Caches.Keys.LineItems;
public static class LineItemsCache<TEntity>
    where TEntity: LineItem
{
    private static readonly string Prefix =
        CacheKeys.LanguagePrefix<TEntity>();


    public static string ByParent(
        long parentId,
        string language,
        int page,
        int pageSize,
        EntityStatus status = EntityStatus.Active)
             => $"{Prefix}:{language}:status:{status}:parent:{parentId}:page:{page}:size:{pageSize}";

    public static string ByParentPattern(
        long parentId,
        EntityStatus status)
           => $"{Prefix}:*:status:{status}:parent:{parentId}:page:*";

    public static string ByParentPattern(
        long parentId)
           => $"{Prefix}:*:parent:{parentId}:page:*";
}
