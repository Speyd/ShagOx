using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Services.Caches.Keys.Dictionaries;
public static class CategoryCache
{
    public static readonly string Prefix =
       CacheKeys.LanguagePrefix<Category>();

    public static string ByProductType(
       long productTypeId,
       string language,
       int page,
       int pageSize,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:{language}:status:{status}:product-type:{productTypeId}:page:{page}:size:{pageSize}";

    public static string ByProductTypePattern(
       long productTypeId,
        EntityStatus status)
           => $"{Prefix}:*:status:{status}:product-type:{productTypeId}:page:*";

    public static string ByProductTypePattern(
       long productTypeId)
           => $"{Prefix}:*:product-type:{productTypeId}:page:*";
}
