using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Caches;
using ShagOxServer.Application.DTOs.Devices.Query;
using ShagOxServer.Application.Interfaces.Repositories.Devices.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Devices.Core.Query;
using ShagOxServer.Application.Services.Base.Query;
using ShagOxServer.Application.Services.Caches.Keys.Devices;
using ShagOxServer.Application.Services.Devices.Core.Mapping;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Devices;
using ShagOxServer.Domain.Filters.Devices;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Devices.Core.Query;
public class DeviceQueryService
    : BaseQueryService<
        DeviceDto,
        Device,
        DeviceSearchFilter
        >,
    IDeviceQueryService
{
    private readonly IDeviceQueryRepository _deviceQueryRepository;


    public DeviceQueryService(
        IDeviceQueryRepository deviceQueryRepository,
        IDataCacheService cacheService,
        IOptions<CacheOptions> cacheOptions
    )
        : base(deviceQueryRepository,
            cacheService,
            cacheOptions)
    {
        _deviceQueryRepository = deviceQueryRepository;
    }


    public override async Task<DeviceDto> ApplyMapperAsync(
        Device entity)
    {
        return DeviceMapper.ToDto(entity);
    }

  
    public async Task<Result<DeviceDto>> GetByIdentifierAsync(
        string identifier,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = DeviceCache.ByIdentifier(
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

    public async Task<Result<Device?>> GetEntityByIdentifierAsync(
        string identifier,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = DeviceCache.EntityByIdentifier(
          identifier,
          status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var device = await _deviceQueryRepository
                    .GetByIdentifierAsync(identifier,
                        cancellationToken, status);

                return Result<Device?>.Success(device);
            },
            _cacheOptions.DefaultExpiration
        );
    }
}
