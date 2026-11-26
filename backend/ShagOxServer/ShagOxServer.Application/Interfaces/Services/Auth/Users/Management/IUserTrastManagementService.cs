using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Management;
public interface IUserTrastManagementService
{
    Task<Result<bool>> UntrustDeviceAsync(
        long deviceId,
        CancellationToken cancellationToken);

    Task<Result<bool>> UntrustDevicesAsync(
        User user,
        bool resetUsedSession = false,
        CancellationToken cancellationToken = default);


    Task RevokeSessionsByUserAsync(
        long userId,
        SessionRevocationReason reason,
        bool resetUsedSession = false,
        CancellationToken cancellationToken = default);

    Task RevokeSessionsByDeviceAsync(
        long deviceId,
        SessionRevocationReason reason,
        bool resetUsedSession = false,
        CancellationToken cancellationToken = default);
}
