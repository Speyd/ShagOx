using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Caches.Keys.Auth;
public static class UserRoleCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<UserRole>();


    public static string RolesByUser(
        long userId,
        int page,
        int pageSize,
        EntityStatus status = EntityStatus.Active)
            => $"{Prefix}:roles:status:{status}:user:{userId}:page:{page}:size:{pageSize}";

    public static string RolesByUserPattern(
        long userId,
        EntityStatus status)
            => $"{Prefix}:roles:status:{status}:user:{userId}:page:*";

    public static string RolesByUserPattern(
        long userId)
            => $"{Prefix}:roles:*:user:{userId}:page:*";

    public static string RoleIdsByUser(
        long userId,
        EntityStatus status = EntityStatus.Active)
            => $"{Prefix}:role-ids:status:{status}:user:{userId}";


    public static string UsersByRole(
        long roleId,
        int page,
        int pageSize,
        EntityStatus status = EntityStatus.Active)
            => $"{Prefix}:users:status:{status}:role:{roleId}:page:{page}:size:{pageSize}";

    public static string UsersByRolePattern(
        long roleId,
        EntityStatus status)
            => $"{Prefix}:users:status:{status}:role:{roleId}:page:*";

    public static string UsersByRolePattern(
        long roleId)
            => $"{Prefix}:users:*:role:{roleId}:page:*";

    public static string UserIdsByRole(
        long roleId,
        EntityStatus status = EntityStatus.Active)
            => $"{Prefix}:role-ids:status:{status}:role:{roleId}";
}
