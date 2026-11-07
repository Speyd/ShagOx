using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Caches.Keys.Specification.Pictures;
public static class AvatarCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<Avatar>();


    public static string ByUser(
       long userId,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:status:{status}:user:{userId}";

    public static string ByUserPattern(
       long userId)
           => $"{Prefix}:*:user:{userId}";

    public static string ByUserPattern(
        EntityStatus status)
           => $"{Prefix}:status:{status}:*";
}
