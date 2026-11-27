using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Management.Trast;
public partial interface IUserTrastManagementService
{
    Task<Result<bool>> RevokeSessionAsync(
        long sessionId,
        SessionRevocationReason reason,
        CancellationToken cancellationToken = default);

    Task<Result<bool>> RevokeSessionByAdminAsync(
        long sessionId,
        SessionRevocationReason reason,
        CancellationToken cancellationToken = default);

    Task<Result<bool>> RevokeSessionsByUserAsync(
        long userId,
        SessionRevocationReason reason,
        bool resetUsedSession = false,
        CancellationToken cancellationToken = default);

    Task<Result<bool>> RevokeSessionsByDeviceAsync(
        long deviceId,
        SessionRevocationReason reason,
        bool resetUsedSession = false,
        CancellationToken cancellationToken = default);
}
