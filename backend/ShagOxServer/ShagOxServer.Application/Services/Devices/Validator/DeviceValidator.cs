using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Devices;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Entities.Devices;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Devices.Validator;
public class DeviceValidator
    : BaseValidator<Device>
{
    private readonly IDeviceExistsRepository _deviceExistsRepository;


    public DeviceValidator(
        IRepository<Device> repository,
        IQueryRepository<Device> deviceRepository,
        IDeviceExistsRepository deviceExistsRepository,
        IDataCacheService cache
    )
        : base(repository,
            deviceRepository,
            deviceExistsRepository,
            cache)
    {
        _deviceExistsRepository = deviceExistsRepository;
    }


    public async Task<Result<bool>> ExistsAsync(
        string deviceIdentifier,
        CancellationToken cancellationToken = default)
    {
        if (!await _deviceExistsRepository
            .ExistsAsync(deviceIdentifier,
                cancellationToken))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.Device);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
        string deviceIdentifier,
        CancellationToken cancellationToken = default)
    {
        if (await _deviceExistsRepository
            .ExistsAsync(deviceIdentifier,
                cancellationToken))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.Device);
        }

        return Result<bool>.Success(true);
    }
}
