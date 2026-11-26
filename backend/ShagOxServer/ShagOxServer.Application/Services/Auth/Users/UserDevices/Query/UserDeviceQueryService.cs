using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Caches;
using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Auth.Users.UserDevices.Mapping;
using ShagOxServer.Application.Services.Base.Query;
using ShagOxServer.Application.Services.Caches.Keys.Auth;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.UserDevices;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Auth.Users.UserDevices.Query;
public class UserDeviceQueryService
    : BaseQueryService<
        UserDeviceDto,
        UserDevice,
        UserDeviceSearchFilter
        >,
    IUserDeviceQueryService
{
    private readonly IUserDeviceQueryRepository _deviceQueryRepository;


    public UserDeviceQueryService(
        IUserDeviceQueryRepository deviceQueryRepository,
        IDataCacheService cacheService,
        IOptions<CacheOptions> cacheOptions
    )
        : base(deviceQueryRepository,
            cacheService,
            cacheOptions)
    {
        _deviceQueryRepository = deviceQueryRepository;
    }


    public override async Task<UserDeviceDto> ApplyMapperAsync(
        UserDevice entity)
    {
        return UserDeviceMapper.ToDto(entity);
    }

    public async Task<Result<PagedResult<UserDeviceDto>>> GetByUserAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = UserDeviceCache.ByUser(
          userId,
          pagination.Page,
          pagination.PageSize,
          status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var devices = await _deviceQueryRepository
                    .GetByUserAsync(userId, pagination,
                        cancellationToken, status);

                return await devices.ToResultPagedAsync(
                    ApplyMapperAsync);
            },
            _cacheOptions.DefaultExpiration
        );
    }

    public async Task<Result<List<UserDeviceDto>>> GetAllByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = UserDeviceCache.AllByUser(
          userId,
          status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var devices = await _deviceQueryRepository
                    .GetAllByUserAsync(userId,
                        cancellationToken, status);

                return await devices.ToResultListAsync(
                    ApplyMapperAsync);
            },
            _cacheOptions.DefaultExpiration
        );
    }

    public async Task<Result<UserDeviceDto>> GetByIdentifierAsync(
        string identifier,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = UserDeviceCache.ByIdentifier(
          identifier,
          status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var device = await _deviceQueryRepository
                    .GetByIdentifierAsync(identifier,
                        cancellationToken, status);

                return await device.ToResultAsync(
                    ApplyMapperAsync);
            },
            _cacheOptions.DefaultExpiration
        );
    }

    public async Task<Result<UserDevice?>> GetEntityByIdentifierAsync(
        string identifier,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = UserDeviceCache.EntityByIdentifier(
          identifier,
          status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var device = await _deviceQueryRepository
                    .GetByIdentifierAsync(identifier,
                        cancellationToken, status);

                return Result<UserDevice?>.Success(device);
            },
            _cacheOptions.DefaultExpiration
        );
    }

    public async Task<Result<PagedResult<UserDeviceDto>>> GetByIpAsync(
        string ipAddress,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = UserDeviceCache.ByIpAddress(
          ipAddress,
          pagination.Page,
          pagination.PageSize,
          status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var device = await _deviceQueryRepository
                    .GetByIpAsync(ipAddress, pagination,
                        cancellationToken, status);

                return await device.ToResultPagedAsync(
                    ApplyMapperAsync);
            },
            _cacheOptions.DefaultExpiration
        );
    }
}
