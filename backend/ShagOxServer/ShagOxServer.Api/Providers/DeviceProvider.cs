using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Api.Providers;
public class DeviceProvider
    : IDeviceProvider
{
    private readonly IUserDeviceQueryRepository _deviceService;
    private readonly IUserContext _userContext;


    public DeviceProvider(
        IUserDeviceQueryRepository deviceService,
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
            .GetByIdentifierAsync(_userContext.DeviceIdentifier,
                cancellationToken, status);

        return result;
    }
}
