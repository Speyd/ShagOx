using ShagOxServer.Application.Interfaces.Services.Auth.UserRoles.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Caches.Auth;
public class RoleCache
{
    private static readonly string Prefix =
           typeof(Role).Name.ToLowerInvariant();

    public static string ByUser(
            long userId,
            int page,
            int pageSize)
            => $"{Prefix}:user:{userId}:page:{page}:size:{pageSize}";

    public static string ByUserPattern(
        long userId)
        => $"{Prefix}:user:{userId}:page:*";

    public static string ByName(
            string name)
            => $"{Prefix}:name:{name}";

    public static async Task InvalidateDeleteAsync(
        ICacheService cache,
        IUserRoleQueryService userRoleService,
        Role cachedEntity)
    {
        await cache.RemoveAsync(
            CacheKeys.Entity<Role>(cachedEntity.Id));

        await cache.RemoveAsync(
            ByName(cachedEntity.Name));

        await InvalidateByUserAsync(cache,
            userRoleService,
            cachedEntity);
    }

    public static async Task InvalidateUpdateAsync(
        ICacheService cache,
        IUserRoleQueryService userRoleService,
        Role cachedEntity)
    {
        await cache.RemoveAsync(
             CacheKeys.Entity<Role>(cachedEntity.Id));

        await cache.RemoveAsync(
            ByName(cachedEntity.Name));

        await InvalidateByUserAsync(cache, 
            userRoleService, 
            cachedEntity);
    }

    private static async Task InvalidateByUserAsync(
        ICacheService cache,
        IUserRoleQueryService userRoleService,
        Role cachedEntity)
    {
        var roleId  = cachedEntity.Id;

        var users = await userRoleService
            .GetUserIdsByRoleAsync(roleId);

        if (!users.IsSuccess)
            return;

        foreach (var userId in users.Value!)
        {
            await cache.RemoveByPatternAsync(
                ByUserPattern(userId));

            await cache.RemoveByPatternAsync(
                UserRoleCache.UsersByRolePattern(roleId));

            await cache.RemoveAsync(
                UserRoleCache.UserIdsByRole(roleId));
        }
    }

}