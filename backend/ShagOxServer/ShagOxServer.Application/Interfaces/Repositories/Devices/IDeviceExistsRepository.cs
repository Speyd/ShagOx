using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Devices;

namespace ShagOxServer.Application.Interfaces.Repositories.Devices;
public interface IDeviceExistsRepository
    : IExistsRepository<Device>
{
    Task<bool> IsBlockedAsync(
        string deviceIdentifier,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(
        string deviceIdentifier,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
