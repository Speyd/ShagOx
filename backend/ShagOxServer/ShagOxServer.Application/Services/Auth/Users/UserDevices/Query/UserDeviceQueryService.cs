using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Caches;
using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Devices.Core.Query;
using ShagOxServer.Application.Services.Auth.Users.UserDevices.Mapping;
using ShagOxServer.Application.Services.Base.Query;
using ShagOxServer.Application.Services.Caches.Keys.Auth;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.UserDevices;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Auth.Users.UserDevices.Query;
public partial class UserDeviceQueryService
    : BaseQueryService<
        UserDeviceDto,
        UserDevice,
        UserDeviceSearchFilter
        >,
    IUserDeviceQueryService
{
    private readonly IUserDeviceQueryRepository _userDeviceQueryRepository;
    private readonly IDeviceQueryService _deviceQueryService;

    protected override bool CacheBySearch => false;

    protected override bool CacheByPaged => false;


    public UserDeviceQueryService(
        IUserDeviceQueryRepository userDeviceQueryRepository,
        IDeviceQueryService deviceQueryService,
        IDataCacheService cacheService,
        IOptions<CacheOptions> cacheOptions
    )
        : base(userDeviceQueryRepository,
            cacheService,
            cacheOptions)
    {
        _userDeviceQueryRepository = userDeviceQueryRepository;
        _deviceQueryService = deviceQueryService;
    }


    public override async Task<UserDeviceDto> ApplyMapperAsync(
        UserDevice entity)
    {
        var deviceDto = await _deviceQueryService
            .ApplyMapperAsync(entity.Device);

        return UserDeviceMapper.ToDto(entity, deviceDto);
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
                var devices = await _userDeviceQueryRepository
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
                var devices = await _userDeviceQueryRepository
                    .GetByUserAndDeviceAsync(userId, deviceId,
                        cancellationToken, status);

                return Result<UserDevice?>.Success(devices);
            },
            _cacheOptions.DefaultExpiration
        );
    }

    public async Task<Result<UserDevice?>> GetEntityByUserAndDeviceNoTrackingAsync(
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
                var devices = await _userDeviceQueryRepository
                    .GetByUserAndDeviceNoTrackingAsync(userId, deviceId,
                        cancellationToken, status);

                return Result<UserDevice?>.Success(devices);
            },
            _cacheOptions.DefaultExpiration
        );
    }
}
