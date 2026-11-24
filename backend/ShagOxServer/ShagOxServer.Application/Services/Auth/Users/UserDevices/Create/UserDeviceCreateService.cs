using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Create;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Auth.Users.UserDevices.Validator;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.UserDevices.Create;
public class UserDeviceCreateService
    : IUserDeviceCreateService
{
    private readonly IRepository<UserDevice> _deviceRepository;
    private readonly UserDeviceValidator _deviceValidator;

    //private readonly RoleInvalidationService _roleInvalid;
    private readonly IDeviceInfoProvider _deviceInfo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserDeviceCreateService> _logger;


    public UserDeviceCreateService(
        IRepository<UserDevice> deviceRepository,
        UserDeviceValidator deviceValidator,
        IDeviceInfoProvider deviceInfo,
        //RoleInvalidationService roleInvalid,
        IUnitOfWork unitOfWork,
        ILogger<UserDeviceCreateService> logger)
    {
        _deviceRepository = deviceRepository;
        _deviceValidator = deviceValidator;
        _deviceInfo = deviceInfo;
        //_roleInvalid = roleInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        UserDeviceCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        await _unitOfWork.BeginTransactionAsync();

        UserDevice device;

        try
        {
            var deviceResult  = await CreateInternalAsync(request);

            if (!deviceResult.IsSuccess ||
                deviceResult.Value is null)
            {
                throw new Exception("Device is null");
            }

            device = deviceResult.Value;

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create device. UserId: {UserId}",
                request.OwnerId);

            return Result<CreateResponse>
                .Fail(EntityErrorResources.UserDeviceCreateFailed);
        }

        //await _roleInvalid.InvalidateCreateAsync(
        //    RoleCacheMapper.ToInfo(role));

        return Result<CreateResponse>.Success(
            new CreateResponse(
                device.Id,
                DateTime.UtcNow
        ));
    }

    public async Task<Result<UserDevice>> CreateInternalAsync(
        UserDeviceCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        var validation = await _deviceValidator
            .NotExistsAsync(request.OwnerId,
                request.DeviceIdentifier,
                cancellationToken);

        if (!validation.IsSuccess)
            return Result<UserDevice>.Fail(validation.Error);

        var deviceInfo = _deviceInfo
            .GetDeviceInfo(request.DeviceIdentifier);

        var device = UserDeviceCreater
            .Create(request, deviceInfo);

        _deviceRepository.Add(device);

        return Result<UserDevice>.Success(device);
    }
}
