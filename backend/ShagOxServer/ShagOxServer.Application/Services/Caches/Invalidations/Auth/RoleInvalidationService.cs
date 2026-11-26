using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Auth.UserInvalid;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Auth;
using ShagOxServer.Domain.Cache.Auth;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Entities.Auth;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Auth;
public class RoleInvalidationService
    : ICacheInvalidationService<Role, RoleCacheContext>
{
    private readonly IUserRoleQueryRepository _userRoleRepository;
    private readonly UserInvalidationService _userInvalid;

    private readonly IDataCacheService _cache;


    public RoleInvalidationService(
        IUserRoleQueryRepository userRoleRepository,
        UserInvalidationService userInvalid,
        IDataCacheService cache)
    {
        _userRoleRepository = userRoleRepository;
        _userInvalid = userInvalid;
        _cache = cache;
    }

    public async Task InvalidateCreateAsync(
        RoleCacheContext entityInfo)
    {
        await InvalidatePatternAsync(entityInfo);
    }

    public async Task InvalidateDeleteAsync(
        RoleCacheContext entityInfo)
    {
        var userInfos = await GetUserInfos(
            entityInfo.Id);

        foreach (var userInfo in userInfos)
        {
            await _cache.RemoveByPatternAsync(RoleCache
                .ByUserPattern(userInfo.Id));

            await _userInvalid
                .InvalidateDeleteAsync(userInfo);
        }

        await InvalidateAsync(entityInfo);
    }

    public async Task InvalidateUpdateAsync(
        RoleCacheContext entityInfo)
    {
        var userInfos = await GetUserInfos(
            entityInfo.Id);

        foreach (var userInfo in userInfos)
        {
            await _cache.RemoveByPatternAsync(RoleCache
                .ByUserPattern(userInfo.Id));

            await _userInvalid
                .InvalidateUpdateAsync(userInfo);
        }

        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
       RoleCacheContext entityInfo)
    {
        var id = entityInfo.Id;

        await _cache.RemoveAsync(CacheKeys.
             Entity<Role>(id));

        await _cache.RemoveAsync(RoleCache.
            ByNamePattern(entityInfo.Name));

        await InvalidatePatternAsync(entityInfo);
    }

    private async Task InvalidatePatternAsync(
        RoleCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
            SearchPattern<Role>(entityInfo.Status));

        await _cache.RemoveByPatternAsync(CacheKeys.
             EntityPagedPattern<Role>(entityInfo.Status));

        await InvalidateUserRoleAsync(entityInfo);
    }

    private async Task<List<UserCacheContext>> GetUserInfos(
        long entityId)
    {
        return await _userRoleRepository
            .GetUserCacheInfoByRoleAsync(entityId);
    }

    private async Task InvalidateUserRoleAsync(
        RoleCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(UserRoleCache
            .UsersByRolePattern(entityInfo.Id));

        await _cache.RemoveAsync(UserRoleCache
            .UserIdsByRole(entityInfo.Id));
    }
}
