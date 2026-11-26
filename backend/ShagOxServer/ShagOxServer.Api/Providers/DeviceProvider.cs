using ShagOxServer.Application.Interfaces.Providers.User;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Api.Providers;
public class DeviceProvider
    : IDeviceProvider
{
    private readonly IUserDeviceQueryService _deviceService;
    private readonly IUserContext _userContext;


    public DeviceProvider(
        IUserDeviceQueryService deviceService,
        IUserContext userContext)
    {
        _deviceService = deviceService;
        _userContext = userContext;
    }


    public async Task<UserDevice?> GetDeviceAsync(
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
