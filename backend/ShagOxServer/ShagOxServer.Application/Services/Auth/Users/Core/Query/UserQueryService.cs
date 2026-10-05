
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Auth.Roles.Query;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.DTOs.Location.Cities.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Query;
using ShagOxServer.Application.Services.Auth.Roles.Mapping;
using ShagOxServer.Application.Services.Auth.Users.Core.Mapping;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Application.Services.Caches.Keys.Auth;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Auth.Users.Core.Query;
public class UserQueryService
    : BaseLocalizedQueryService<
        UserDto,
        User,
        UserSearchFilter
        >,
    IUserQueryService
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IRoleQueryRepository _roleQueryRepository;
    private readonly ICityQueryService _cityService;


    private readonly IUserContext _context;


    public UserQueryService(
        IUserQueryRepository userQueryRepository,
        IRoleQueryRepository roleQueryRepository,
        ICityQueryService cityService,
        IUserContext userContext,
        ICacheService cache,
        IOptions<CacheSettings> settings,
        ILanguageProvider language
    )
        : base(userQueryRepository, language, cache, settings)
    {
        _userQueryRepository = userQueryRepository;
        _roleQueryRepository = roleQueryRepository;
        _cityService = cityService;
        _context = userContext;
    }


    public override async Task<UserDto> ApplyMapperAsync(
        User entity)
    {
        CityDto? cityDto = null;
        if (entity.City is not null)
        {
            cityDto = await _cityService
            .ApplyMapperAsync(entity.City);
        }

        return UserMapper.ToDto(entity, cityDto);
    }

    public async Task<Result<UserShortDto>> GetByContactAsync(
        string value)
    {
        var cacheKey = UserCache.ByContact(
            value,
            _language.Language);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var user = await _userQueryRepository
                    .GetByContactAsync(value);

                return user.ToResult(UserShortMapper.ToDto);
            },
            _settings.KeyExpiration
        );
    }

    public async Task<Result<UserDto>> GetMyProfileAsync()
    {
        var cacheKey = GetCacheKey(_context.UserId);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var user = await _userQueryRepository
                    .GetByIdAsync(_context.UserId);

                return await user.ToResultAsync(ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }

    public async Task<Result<PagedResult<RoleDto>>> GetMyRoleAsync(
        PaginationParams pagination)
    {
        var cacheKey = UserRoleCache.RolesByUser(
            _context.UserId,
            pagination.Page,
            pagination.PageSize);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var roles = await _roleQueryRepository
                    .GetByUserAsync(_context.UserId, pagination);

                return roles.ToResultPaged(RoleMapper.ToDto);
            },
            _settings.KeyExpiration
        );
    }
}