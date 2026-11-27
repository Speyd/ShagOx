using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Core;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
using ShagOxServer.Application.Interfaces.Repositories.Devices;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Services.Devices.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Devices.Management;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Devices.Management;
public class DeviceModerationService
    : IDeviceModerationService
{
    private readonly IDeviceQueryService _deviceService;
    private readonly IDeviceCommandsRepository _deviceCommandsRepository;


    public DeviceModerationService(
        IDeviceQueryService deviceService,
        IDeviceCommandsRepository deviceCommandsRepository)
    {
        _deviceService = deviceService;
        _deviceCommandsRepository = deviceCommandsRepository;
    }


    public async Task<Result<bool>> BanDeviceAsync(
        long deviceId,
        DateTime? blockedUntil,
        CancellationToken cancellationToken)
    {
        var device = await _deviceService.GetByIdAsync(
            deviceId,
            cancellationToken);

        if (!device.IsSuccess)
        {
            return Result<bool>.Fail(device.Error);
        }

        await _deviceCommandsRepository.BanDeviceAsync(
            deviceId,
            blockedUntil,
            cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> UnbanDeviceAsync(
        long deviceId,
        CancellationToken cancellationToken)
    {
        var device = await _deviceService.GetByIdAsync(
            deviceId,
            cancellationToken);

        if (!device.IsSuccess)
        {
            return Result<bool>.Fail(device.Error);
        }

        await _deviceCommandsRepository.UnbanDeviceAsync(
            deviceId,
            cancellationToken);

        return Result<bool>.Success(true);
    }
}
