using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Auth.Roles;
using ShagOxServer.Application.DTOs.Auth.Roles.Cache;
using ShagOxServer.Application.DTOs.Auth.UserRoles;
using ShagOxServer.Application.DTOs.Auth.Users.Core;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.UserRoles.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Auth.Roles.Mapping;
using ShagOxServer.Application.Services.Auth.UserRoles.Mapping;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Caches.Auth;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.UserRoles;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Auth.UserRoles.Query;
public class UserRoleQueryService 
    : BaseQueryService<
        UserRoleDto,
        UserRole,
        UserRoleSearchFilter
        >,
    IUserRoleQueryService
{
    private readonly IUserRoleQueryRepository _userRoleQueryRepository;
    private readonly IUserQueryService _userService;
    private readonly IRoleQueryService _roleService;



    public UserRoleQueryService(
        IUserRoleQueryRepository userRoleQueryRepository,
        IUserQueryService userService,
        IRoleQueryService roleService,
        ICacheService cache,
        IOptions<CacheSettings> settings
    )
        : base(userRoleQueryRepository, cache, settings)
    {
        _userRoleQueryRepository = userRoleQueryRepository;
        _userService = userService;
        _roleService = roleService;
    }


    public override async Task<UserRoleDto> ApplyMapperAsync(
        UserRole entity)
    {
        return UserRoleMapper.ToDto(entity);
    }

    public async Task<Result<PagedResult<RoleDto>>> GetRolesByUserAsync(
        long userId,
		PaginationParams pagination)
    {
        var cacheKey = UserRoleCache.RolesByUser(
           userId,
           pagination.Page,
           pagination.PageSize);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var roles = await _userRoleQueryRepository
                    .GetRolesByUserAsync(userId, pagination);

                return await roles.ToResultPagedAsync(
                    _roleService.ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }

    public async Task<Result<List<RoleCacheInfo>>> GetRolesCacheInfoByUserAsync(
       long userId)
    {
        var cacheKey = UserRoleCache.RoleIdsByUser(userId);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var roles = await _userRoleQueryRepository
                    .GetRoleCacheInfoByUserAsync(userId);

                return Result<List<RoleCacheInfo>>.Success(roles);
            },
            _settings.KeyExpiration
        );
    }

    public async Task<Result<PagedResult<UserDto>>> GetUsersByRoleAsync(
        long roleId,
		PaginationParams pagination)
    {
        var cacheKey = UserRoleCache.UsersByRole(
           roleId,
           pagination.Page,
           pagination.PageSize);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var users = await _userRoleQueryRepository
                    .GetUsersByRoleAsync(roleId, pagination);

                return await users.ToResultPagedAsync(
                    _userService.ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }

    public async Task<Result<List<UserCacheInfo>>> GetUserCacheInfoByRoleAsync(
       long roleId)
    {
        var cacheKey = UserRoleCache.UserIdsByRole(roleId);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var users = await _userRoleQueryRepository
                    .GetUserCacheInfoByRoleAsync(roleId);

                return Result<List<UserCacheInfo>>.Success(users);
            },
            _settings.KeyExpiration
        );
    }
}