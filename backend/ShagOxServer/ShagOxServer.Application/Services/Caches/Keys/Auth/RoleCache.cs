using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Caches.Keys.Auth;
public static class RoleCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<Role>();


    public static string ByUser(
        long userId,
        int page,
        int pageSize,
        EntityStatus status = EntityStatus.Active)
            => $"{Prefix}:status:{status}:user:{userId}:page:{page}:size:{pageSize}";

    public static string ByUserPattern(
        long userId,
        EntityStatus status)
            => $"{Prefix}:status:{status}:user:{userId}:page:*";

    public static string ByUserPattern(
        long userId)
            => $"{Prefix}:*:user:{userId}:page:*";

    public static string ByName(
        string name,
        EntityStatus status)
            => $"{Prefix}:status:{status}:name:{name}";

    public static string ByNamePattern(
        string name)
            => $"{Prefix}:*:name:{name}";
}
