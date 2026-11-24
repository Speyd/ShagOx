using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.UserDevices.Validator;
public class UserDeviceValidator
    : BaseValidator<UserDevice>
{
    private readonly IUserDeviceExistsRepository _deviceExistsRepository;


    public UserDeviceValidator(
        IRepository<UserDevice> repository,
        IQueryRepository<UserDevice> deviceRepository,
        IUserDeviceExistsRepository deviceExistsRepository,
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
        long userId,
        string deviceIdentifier,
        CancellationToken cancellationToken = default)
    {
        if (!await _deviceExistsRepository
            .ExistsAsync(userId, deviceIdentifier,
                cancellationToken))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.UserDevice);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
        long userId,
        string deviceIdentifier,
        CancellationToken cancellationToken = default)
    {
        if (await _deviceExistsRepository
            .ExistsAsync(userId, deviceIdentifier,
                cancellationToken))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.UserDevice);
        }

        return Result<bool>.Success(true);
    }
}
