using ShagOxServer.Application.DTOs.Auth.Roles.Cache;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Caches.Auth;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Auth;
public class RoleInvalidationService
    : ICacheInvalidationService<Role, RoleCacheInfo>
{
    private readonly IUserRoleQueryRepository _userRoleRepository;
    private readonly UserInvalidationService _userInvalid;

    private readonly ICacheService _cache;


    public RoleInvalidationService(
        IUserRoleQueryRepository userRoleRepository,
        UserInvalidationService userInvalid,
        ICacheService cache)
    {
        _userRoleRepository = userRoleRepository;
        _userInvalid = userInvalid;
        _cache = cache;
    }


    public async Task InvalidateDeleteAsync(
        RoleCacheInfo entityInfo)
    {
        var id = entityInfo.Id;

        await _cache.RemoveAsync(CacheKeys.
             Entity<Role>(id));

        await _cache.RemoveAsync(RoleCache.
            ByName(entityInfo.Name));

        var userInfos = await GetUserInfos(id);

        foreach (var userInfo in userInfos)
        {
            await _userInvalid
                .InvalidateDeleteAsync(userInfo);
        }

        await InvalidateUserRoleAsync(id);
    }

    public async Task InvalidateUpdateAsync(
        RoleCacheInfo entityInfo)
    {
        var id = entityInfo.Id;

        await _cache.RemoveAsync(CacheKeys.
             Entity<Role>(id));

        await _cache.RemoveAsync(RoleCache.
           ByName(entityInfo.Name));

        var userInfos = await GetUserInfos(id);

        foreach (var userInfo in userInfos)
        {
            await _userInvalid
                .InvalidateUpdateAsync(userInfo);
        }

        await InvalidateUserRoleAsync(id);
    }

    private async Task<List<UserCacheInfo>> GetUserInfos(
        long entityId)
    {
        return await _userRoleRepository
            .GetUserCacheInfoByRoleAsync(entityId);
    }

    private async Task InvalidateUserRoleAsync(
        long entityId)
    {
        await _cache.RemoveByPatternAsync(
                UserRoleCache.UsersByRolePattern(entityId));

        await _cache.RemoveAsync(
            UserRoleCache.UserIdsByRole(entityId));
    }
}