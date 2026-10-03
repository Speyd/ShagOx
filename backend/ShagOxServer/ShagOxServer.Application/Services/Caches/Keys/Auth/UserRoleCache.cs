using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Caches.Keys.Auth;
public static class UserRoleCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<UserRole>();


    public static string RolesByUser(
        long userId,
        int page,
        int pageSize)
            => $"{Prefix}:roles:user:{userId}:page:{page}:size:{pageSize}";

    public static string RolesByUserPattern(
        long userId)
            => $"{Prefix}:roles:user:{userId}:page:*";

    public static string RoleIdsByUser(
        long userId)
            => $"{Prefix}:role-ids:user:{userId}";



    public static string UsersByRole(
        long roleId,
        int page,
        int pageSize)
            => $"{Prefix}:users:role:{roleId}:page:{page}:size:{pageSize}";

    public static string UsersByRolePattern(
        long roleId)
            => $"{Prefix}:users:role:{roleId}:page:*";

    public static string UserIdsByRole(
        long roleId)
            => $"{Prefix}:role-ids:role:{roleId}";
}