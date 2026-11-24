using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Management;
public interface IUserTrastManagementService
{
    Task TrustDeviceAsync(
        User user);

    Task TrustSessionByUserAsync(
        User user,
        SessionRevocationReason reason,
        bool resetUsedSession = false);

    Task TrustSessionByDeviceAsync(
        UserDevice user,
        SessionRevocationReason reason,
        bool resetUsedSession = false);
}
