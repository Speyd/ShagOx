using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Devices.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Providers.User;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Devices.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Devices.Validator;
using ShagOxServer.Domain.Entities.Devices;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Devices.Create;
public class DeviceCreateService
    : IDeviceCreateService
{
    private readonly IRepository<Device> _deviceRepository;
    private readonly DeviceValidator _deviceValidator;

    private readonly IDeviceInfoProvider _deviceInfo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeviceCreateService> _logger;


    public DeviceCreateService(
        IRepository<Device> deviceRepository,
        DeviceValidator deviceValidator,
        IDeviceInfoProvider deviceInfo,
        IUnitOfWork unitOfWork,
        ILogger<DeviceCreateService> logger)
    {
        _deviceRepository = deviceRepository;
        _deviceValidator = deviceValidator;
        _deviceValidator = deviceValidator;
        _deviceInfo = deviceInfo;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        DeviceCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var deviceResult = await CreateInternalAsync(request);

            if (!deviceResult.IsSuccess ||
                deviceResult.Value is null)
            {
                return Result<CreateResponse>
                    .Fail(deviceResult.Error);
            }

            await _unitOfWork.CommitAsync();

            return Result<CreateResponse>.Success(
            new CreateResponse(
                deviceResult.Value.Id,
                DateTime.UtcNow
        ));
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create device. DeviceIdentifier: {DeviceIdentifier}",
                request.DeviceIdentifier);

            return Result<CreateResponse>
                .Fail(EntityErrorResources.DeviceCreateFailed);
        }
    }

    public async Task<Result<Device>> CreateInternalAsync(
        DeviceCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        var idenExists = await _deviceValidator.ExistsAsync(
            request.DeviceIdentifier, cancellationToken);

        if (!idenExists.IsSuccess)
        {
            return Result<Device>
                .Fail(idenExists.Error);
        }

        var deviceInfo = _deviceInfo
            .GetDeviceInfo(request.DeviceIdentifier);

        var device = DeviceCreater
            .Create(request, deviceInfo);

        _deviceRepository.Add(device);

        return Result<Device>.Success(device);
    }
}
