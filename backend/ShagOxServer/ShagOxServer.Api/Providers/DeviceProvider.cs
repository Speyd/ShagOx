using ShagOxServer.Application.Interfaces.Providers.Devices;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Devices.Core.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Devices;

namespace ShagOxServer.Api.Providers;
public class DeviceProvider
    : IDeviceProvider
{
    private readonly IUserDeviceQueryService _userDeviceService;
    private readonly IDeviceQueryService _deviceService;
    private readonly IUserContext _userContext;


    public DeviceProvider(
        IUserDeviceQueryService userDeviceService,
        IDeviceQueryService deviceService,
        IUserContext userContext)
    {
        _userDeviceService = userDeviceService;
        _deviceService = deviceService;
        _userContext = userContext;
    }


    public async Task<UserDevice?> GetUserDeviceAsync(
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var device = await GetDeviceAsync(cancellationToken, status);
        var userId = _userContext.UserId;

        if (device is null || !userId.HasValue)
            return null;

        var result = await _userDeviceService
            .GetEntityByUserAndDeviceAsync(
                userId.Value, device.Id,
                cancellationToken, status);

        return result.IsSuccess ? result.Value : null;
    }

    public async Task<Device?> GetDeviceAsync(
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        if (_userContext.DeviceIdentifier is null)
            return null;

        var result = await _deviceService
            .GetEntityByIdentifierAsync(_userContext.DeviceIdentifier,
                cancellationToken, status);

        return result.IsSuccess ? result.Value : null;
    }
}
