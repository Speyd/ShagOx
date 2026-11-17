using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Caches.Keys.Specification.Pictures;
public static class CategoryImageCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<Avatar>();


    public static string ByCategory(
       long categoryId,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:status:{status}:category:{categoryId}";

    public static string ByCategoryPattern(
       long categoryId)
           => $"{Prefix}:*:category:{categoryId}";

    public static string ByCategoryPattern(
        EntityStatus status)
           => $"{Prefix}:status:{status}:*";
}
