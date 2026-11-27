using ShagOxServer.Application.DTOs.Auth.Users.UserDevices;
using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Create;
using ShagOxServer.Application.Interfaces.Providers.Devices;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Create;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Resources.Auth.Logins;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.Domain.Entities.Devices.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.UserDevices;
public class UserDeviceAuthenticationService
    : IUserDeviceAuthenticationService
{
    private readonly IDeviceProvider _deviceProvider;
    private readonly IUserContext _userContext;
    private readonly IUserDeviceCreateService _deviceCreateService;
    private readonly IUserDeviceQueryService _deviceQueryService;


    public UserDeviceAuthenticationService(
        IDeviceProvider deviceProvider,
        IUserContext userContext,
        IUserDeviceCreateService deviceCreateService,
        IUserDeviceQueryService deviceQueryService)
    {
        _deviceProvider = deviceProvider;
        _userContext = userContext;
        _deviceCreateService = deviceCreateService;
        _deviceQueryService = deviceQueryService;
    }


    public async Task<Result<UserDeviceAuthenticationResponse>>
        AuthenticateDeviceAsync(User user)
    {
        var device = await _deviceProvider.GetDeviceAsync();

        if (device is null)
        {
            return Result<UserDeviceAuthenticationResponse>.NotFound(
                EntityNamesResources.Device);
        }
        else if (device.Status != DeviceStatus.Active)
        {
            return Result<UserDeviceAuthenticationResponse>.Fail(
                LoginAuthResources.DeviceBlocked);
        }



        var userDevice = await _deviceQueryService
            .GetByUserAndDeviceAsync(user.Id, device.Id);

        if (userDevice.IsSuccess)
        {
            var deviceValue = userDevice.Value;

            if (deviceValue!.Status != UserDeviceStatus.Active ||
                deviceValue!.Device.Id != device.Id)
            {
                return Result<UserDeviceAuthenticationResponse>.Fail(
                    LoginAuthResources.DeviceBlocked);
            }

            return Result<UserDeviceAuthenticationResponse>.Success(
                new UserDeviceAuthenticationResponse(
                    deviceValue!.Id,
                    deviceValue!.IsTrusted));
        }      


        var identifier = _userContext.DeviceIdentifier;

        if (string.IsNullOrWhiteSpace(identifier))
        {
            return Result<UserDeviceAuthenticationResponse>.Fail(
                LoginAuthResources.DeviceIdentifierRequired);
        }

        var result = await _deviceCreateService.CreateAsync(
            new UserDeviceCreateRequest(
                user.Id,
                device.Id));

        if (!result.IsSuccess)
        {
            return Result<UserDeviceAuthenticationResponse>.Fail(
                result.Error);
        }

        return Result<UserDeviceAuthenticationResponse>.Success(
            new UserDeviceAuthenticationResponse(
                result.Value!.Id,
                false));
    }
}
