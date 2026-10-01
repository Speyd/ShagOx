using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;
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

    public static string ByUserPattern(long userId)
        => $"{Prefix}:user:{userId}:page:*";

    public static string ByName(
            string name)
            => $"{Prefix}:name:{name}";

    public static async Task InvalidateDeleteAsync(
        ICacheService cache,
        IUserRoleQueryRepository userRoleRepository,
        Role cachedEntity)
    {
        await cache.RemoveAsync(
            CacheKeys.Entity<Role>(cachedEntity.Id));

        await cache.RemoveByPatternAsync(
            ByName(cachedEntity.Name));

        var users = await userRoleRepository
            .GetUsersByRoleIdAsync(cachedEntity.Id);

        foreach (var userId in users)
        {
            await cache.RemoveByPatternAsync(
                ByUserPattern(userId));
        }
    }

    public static async Task InvalidateUpdateAsync(
        ICacheService cache,
        IUserRoleQueryRepository userRoleRepository,
        Role cachedEntity)
    {
        await cache.RemoveAsync(
            CacheKeys.Entity<Role>(cachedEntity.Id));

        var users = await userRoleRepository
            .GetUsersByRoleIdAsync(cachedEntity.Id);

        foreach (var userId in users)
        {
            await cache.RemoveByPatternAsync(
                ByUserPattern(userId));
        }
    }
}