using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.Interfaces.Repositories.Devices;
public interface IDeviceExistsRepository
{
    Task<bool> IsBlockedAsync(
        string deviceIdentifier,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(
        string deviceIdentifier,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
