using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Services.Auth.UserRoles.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Caches.Advertisements;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Caches.Auth;
public class UserCache
{
    private static readonly string Prefix =
           typeof(User).Name.ToLowerInvariant();


    public static string ByContact(
        string contact)
            => $"{Prefix}:contact:{contact}";

    public static string ByContactPattern(
        long userId)
            => $"{Prefix}:contact:*";


    public static async Task InvalidateDeleteAsync(
        ICacheService cache,
        IUserRoleQueryService userRoleService,
        User cachedEntity)
    {
        await cache.RemoveAsync(
            CacheKeys.Entity<User>(cachedEntity.Id));

        await InvalidateByContactAsync(cache, cachedEntity);

        await InvalidateUserRoleAsync(cache,
           userRoleService,
           cachedEntity.Id);
    }

    public static async Task InvalidateUpdateAsync(
        ICacheService cache,
        IUserRoleQueryService userRoleService,
        User cachedEntity)
    {
        await cache.RemoveAsync(
             CacheKeys.Entity<User>(cachedEntity.Id));

        await InvalidateByContactAsync(cache, cachedEntity);

        await InvalidateUserRoleAsync(cache, 
            userRoleService, 
            cachedEntity.Id);
    }

    private static async Task InvalidateByContactAsync(
        ICacheService cache,
        User cachedEntity)
    {
        var id = cachedEntity.Id;

        if (!string.IsNullOrEmpty(cachedEntity.Phone))
        {
            await cache.RemoveAsync(
                ByContact(cachedEntity.Phone));
        }

        if (!string.IsNullOrEmpty(cachedEntity.Email))
        {
            await cache.RemoveAsync(
                ByContact(cachedEntity.Email));
        }

        await cache.RemoveAsync(
            ByContact(cachedEntity.UserName));
    }

    private static async Task InvalidateUserRoleAsync(
        ICacheService cache,
        IUserRoleQueryService userRoleService,
        long userId)
    {
        await cache.RemoveByPatternAsync(
            UserRoleCache.RolesByUserPattern(userId));

        await cache.RemoveAsync(
            UserRoleCache.RoleIdsByUser(userId));
    }

    public static async Task InvalidateByRoleAsync(
        ICacheService cache,
        IUserRoleQueryService userRoleService,
        IAdvertisementQueryRepository advertRepository,
        long roleId)
    {
        var users = await userRoleService
            .GetRolesIdsByUserAsync(roleId);

        if (!users.IsSuccess)
            return;

        foreach (var userId in users.Value!)
        {
            await AdvertisementCache
                .InvalidateByUserAsync(cache, 
                    advertRepository, 
                    userId);

            await cache.RemoveByPatternAsync(
                ByContactPattern(userId));

            await InvalidateUserRoleAsync(cache, 
                userRoleService, 
                userId);
        }
    }
}