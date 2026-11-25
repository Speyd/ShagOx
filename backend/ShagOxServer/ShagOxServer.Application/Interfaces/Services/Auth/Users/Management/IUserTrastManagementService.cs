using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Management;
public interface IUserTrastManagementService
{
    Task<Result<bool>> UntrustDeviceAsync(
        long deviceId,
        CancellationToken cancellationToken);

    Task UntrustDevicesAsync(
        User user,
        bool resetUsedSession = false,
        CancellationToken cancellationToken = default);


    Task RevokeSessionsByUserAsync(
        User user,
        SessionRevocationReason reason,
        bool resetUsedSession = false,
        CancellationToken cancellationToken = default);

    Task RevokeSessionsByDeviceAsync(
        UserDevice user,
        SessionRevocationReason reason,
        bool resetUsedSession = false,
        CancellationToken cancellationToken = default);
}
