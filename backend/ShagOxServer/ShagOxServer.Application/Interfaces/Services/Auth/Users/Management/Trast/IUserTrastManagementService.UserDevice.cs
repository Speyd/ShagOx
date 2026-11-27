using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Management.Trast;
public partial interface IUserTrastManagementService
{
    Task<Result<bool>> UntrustDeviceAsync(
        long deviceId,
        CancellationToken cancellationToken);

    Task<Result<bool>> UntrustDeviceByAdminAsync(
        long deviceId,
        CancellationToken cancellationToken);

    Task<Result<bool>> UntrustDevicesAsync(
        User user,
        bool resetUsedSession = false,
        CancellationToken cancellationToken = default);
}
