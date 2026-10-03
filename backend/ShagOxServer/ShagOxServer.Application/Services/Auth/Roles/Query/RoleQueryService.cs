using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Auth.Roles;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Auth.Roles.Mapping;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Caches.Keys.Auth;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.Roles;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Auth.Roles.Query;
public class RoleQueryService 
    : BaseQueryService<
        RoleDto,
        Role,
        RoleSearchFilter
        >,
    IRoleQueryService
{
    private readonly IRoleQueryRepository _roleQueryRepository;


    public RoleQueryService(
        IRoleQueryRepository roleQueryRepository,
        ICacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(roleQueryRepository, cacheService, settings)
    {
        _roleQueryRepository = roleQueryRepository;
    }


    public override async Task<RoleDto> ApplyMapperAsync(
        Role entity)
    {
        return RoleMapper.ToDto(entity);
    }

    public async Task<Result<PagedResult<RoleDto>>> GetByUserAsync(
       long userId,
       PaginationParams pagination)
    {
        var cacheKey = RoleCache.ByUser(
           userId,
           pagination.Page,
           pagination.PageSize);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var roles = await _roleQueryRepository
                    .GetByUserAsync(userId, pagination);

                return await roles.ToResultPagedAsync(
                    ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }

    public async Task<Result<RoleDto>> GetByNameAsync(
        string name)
    {
        var cacheKey = RoleCache.ByName(name);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var role = await _roleQueryRepository
                    .GetByNameAsync(name);

                return await role.ToResultAsync(
                    ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }
}