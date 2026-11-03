using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Auth.Roles.Cache;
using ShagOxServer.Application.DTOs.Auth.Roles.Query;
using ShagOxServer.Application.DTOs.Auth.UserRoles;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Cache;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.UserRoles.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Auth.UserRoles.Mapping;
using ShagOxServer.Application.Services.Base.Query;
using ShagOxServer.Application.Services.Caches.Keys.Auth;
using ShagOxServer.Domain.Base.Enum;
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
        IDataCacheService cache,
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
		PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = UserRoleCache.RolesByUser(
           userId,
           pagination.Page,
           pagination.PageSize, 
           status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var roles = await _userRoleQueryRepository
                    .GetRolesByUserAsync(userId, pagination,
                        cancellationToken, status);

                return await roles.ToResultPagedAsync(
                    _roleService.ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }

    public async Task<Result<List<RoleCacheInfo>>> GetRolesCacheInfoByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = UserRoleCache.RoleIdsByUser(
            userId, 
            status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var roles = await _userRoleQueryRepository
                    .GetRoleCacheInfoByUserAsync(userId,
                        cancellationToken, status);

                return Result<List<RoleCacheInfo>>.Success(roles);
            },
            _settings.KeyExpiration
        );
    }

    public async Task<Result<PagedResult<UserDto>>> GetUsersByRoleAsync(
        long roleId,
		PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = UserRoleCache.UsersByRole(
           roleId,
           pagination.Page,
           pagination.PageSize, 
           status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var users = await _userRoleQueryRepository
                    .GetUsersByRoleAsync(roleId, pagination,
                        cancellationToken, status);

                return await users.ToResultPagedAsync(
                    _userService.ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }

    public async Task<Result<List<UserCacheInfo>>> GetUserCacheInfoByRoleAsync(
        long roleId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = UserRoleCache.UserIdsByRole(
            roleId, 
            status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var users = await _userRoleQueryRepository
                    .GetUserCacheInfoByRoleAsync(roleId,
                        cancellationToken, status);

                return Result<List<UserCacheInfo>>.Success(users);
            },
            _settings.KeyExpiration
        );
    }
}
