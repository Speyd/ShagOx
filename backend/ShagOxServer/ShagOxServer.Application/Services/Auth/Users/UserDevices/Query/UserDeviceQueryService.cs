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


    public async Task<Result<UserDeviceDto>> GetByUserAndDeviceAsync(
        long userId,
        long deviceId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = UserDeviceCache.ByUserAndDevice(
          userId,
          deviceId,
          status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var devices = await _deviceQueryRepository
                    .GetByUserAndDeviceAsync(userId, deviceId,
                        cancellationToken, status);

                return await devices.ToResultAsync(
                    ApplyMapperAsync);
            },
            _cacheOptions.DefaultExpiration
        );
    }

    public async Task<Result<UserDevice?>> GetEntityByUserAndDeviceAsync(
        long userId,
        long deviceId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = UserDeviceCache.EntityByUserAndDevice(
          userId,
          deviceId,
          status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var devices = await _deviceQueryRepository
                    .GetByUserAndDeviceAsync(userId, deviceId,
                        cancellationToken, status);

                return Result<UserDevice?>.Success(devices);
            },
            _cacheOptions.DefaultExpiration
        );
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

    public async Task<Result<PagedResult<UserDeviceDto>>> GetByDeviceAsync(
       long deviceId,
       PaginationParams pagination,
       CancellationToken cancellationToken = default,
       EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = UserDeviceCache.ByDevice(
          deviceId,
          pagination.Page,
          pagination.PageSize,
          status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var devices = await _deviceQueryRepository
                    .GetByDeviceAsync(deviceId, pagination,
                        cancellationToken, status);

                return await devices.ToResultPagedAsync(
                    ApplyMapperAsync);
            },
            _cacheOptions.DefaultExpiration
        );
    }

    public async Task<Result<List<UserDeviceDto>>> GetAllByDeviceAsync(
        long deviceId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = UserDeviceCache.AllByDevice(
          deviceId,
          status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var devices = await _deviceQueryRepository
                    .GetAllByDeviceAsync(deviceId,
                        cancellationToken, status);

                return await devices.ToResultListAsync(
                    ApplyMapperAsync);
            },
            _cacheOptions.DefaultExpiration
        );
    }
}
