using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users;
public interface IUserTrastManagementService
{
    Task TrustDeviceAsync(
        User user);

    Task TrustSessionAsync(
        User user,
        SessionRevocationReason reason,
        bool resetUsedSession = false);
}
