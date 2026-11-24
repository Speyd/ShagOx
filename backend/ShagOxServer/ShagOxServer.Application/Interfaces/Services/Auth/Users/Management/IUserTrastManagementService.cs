using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Management;
public interface IUserTrastManagementService
{
    Task RevokeDeviceAsync(
        User user,
        bool resetUsedSession = false);

    Task RevokeSessionByUserAsync(
        User user,
        SessionRevocationReason reason,
        bool resetUsedSession = false);

    Task RevokeSessionByDeviceAsync(
        UserDevice user,
        SessionRevocationReason reason,
        bool resetUsedSession = false);
}
