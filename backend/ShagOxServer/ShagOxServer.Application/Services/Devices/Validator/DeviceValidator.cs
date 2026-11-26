using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Devices;
using ShagOxServer.Application.Interfaces.Repositories.Devices.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.EntityNames.Extensions;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Devices;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Devices.Validator;
public class DeviceValidator
    : BaseValidator<Device>
{
    private readonly IDeviceExistsRepository _deviceExistsRepository;
    private readonly IDeviceQueryRepository _deviceQueryRepository;


    public DeviceValidator(
        IRepository<Device> repository,
        IDeviceQueryRepository deviceQueryRepository,
        IDeviceExistsRepository deviceExistsRepository,
        IDataCacheService cache
    )
        : base(repository,
            deviceQueryRepository,
            deviceExistsRepository,
            cache)
    {
        _deviceExistsRepository = deviceExistsRepository;
        _deviceQueryRepository = deviceQueryRepository;
    }


    public async Task<Result<Device>> GetByIdentifierAsync(
       string identifier,
       CancellationToken cancellationToken = default,
       EntityStatus findStatus = EntityStatus.ActiveAndHidden)
    {
        var entity = await _deviceQueryRepository
            .GetByIdentifierAsync(identifier, 
                cancellationToken, findStatus);

        if (entity is null)
        {
            return Result<Device>.NotFound(
                EntityNamesResources.Device);
        }

        return Result<Device>.Success(entity);
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
            return Result<bool>.AlreadyExists(
                EntityNamesResources.Device);
        }

        return Result<bool>.Success(true);
    }
}
