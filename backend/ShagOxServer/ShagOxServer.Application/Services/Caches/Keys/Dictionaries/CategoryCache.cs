using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Services.Caches.Keys.Dictionaries;
public class CategoryCache
{
    public static readonly string Prefix =
       CacheKeys.LanguagePrefix<Category>();

    public static string ByProductType(
       long productTypeId,
       string language,
       int page,
       int pageSize)
           => $"{Prefix}:{language}:product-type:{productTypeId}:page:{page}:size:{pageSize}";

    public static string ByProductTypePattern(
       long productTypeId)
           => $"{Prefix}:*product-type:{productTypeId}:page:*";
}