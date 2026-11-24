using ShagOxServer.Application.Interfaces.Providers.User;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions;
using ShagOxServer.Application.Interfaces.Services.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;

namespace ShagOxServer.Application.Services.Auth.Users;
public class UserTrastManagementService
    : IUserTrastManagementService
{
    private readonly IDeviceProvider _deviceProvider;
    private readonly IUserDeviceCommandRepository _deviceCommandRepository;
    private readonly IUserDeviceQueryRepository _deviceQueryRepository;

    private readonly ISessionProvider _sessionProvider;
    private readonly IUserSessionCommandRepository _sessionCommandRepository;
    private readonly IUserSessionQueryRepository _sessionQueryRepository;


    public UserTrastManagementService(
        IDeviceProvider deviceProvider,
        IUserDeviceCommandRepository deviceCommandRepository,
        IUserDeviceQueryRepository deviceQueryRepository,
        ISessionProvider sessionProvider,
        IUserSessionCommandRepository sessionCommandRepository,
        IUserSessionQueryRepository sessionQueryRepository)
    {
        _deviceProvider = deviceProvider;
        _deviceCommandRepository = deviceCommandRepository;
        _deviceQueryRepository = deviceQueryRepository;
        _sessionProvider = sessionProvider;
        _sessionCommandRepository = sessionCommandRepository;
        _sessionQueryRepository = sessionQueryRepository;
    }

    public async Task TrustDeviceAsync(
        User user)
    {
        var usedDevice = await _deviceProvider
            .GetDeviceAsync();

        if (usedDevice is not null)
        {
            await _deviceCommandRepository
                .MarkAsTrasteAsync(usedDevice.Id);
        }

        var userDevices = await _deviceQueryRepository
            .GetAllByUserAsync(user.Id);

        foreach (var device in userDevices)
        {
            if (device.Id == usedDevice?.Id)
                continue;

            await _deviceCommandRepository
                .UnmarkAsTrasteAsync(device.Id);
        }
    }

    public async Task TrustSessionAsync(
        User user,
        SessionRevocationReason reason,
        bool resetUsedSession = false)
    {
        var usedSession = await _sessionProvider
            .GetSessionAsync();

        if (usedSession is not null && !resetUsedSession)
        {
            await _sessionCommandRepository
                .RevokeSessionAsync(usedSession.Id, reason);
        }

        var userSessions = await _sessionQueryRepository
            .GetAllByUserAsync(user.Id);

        foreach (var session in userSessions)
        {
            if (session.Id == usedSession?.Id)
                continue;

            await _sessionCommandRepository
                .RevokeSessionAsync(session.Id, reason);
        }
    }
}
