using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Devices;
using ShagOxServer.Domain.Filters.Devices;

namespace ShagOxServer.Application.Interfaces.Repositories.Devices.Query;
public partial interface IDeviceQueryRepository
    : ISearchRepository<Device, DeviceSearchFilter>
{
    Task<Device?> GetByIdentifierAsync(
        string deviceIdentifier,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<Device?> GetByIdentifierNoTrackingAsync(
        string deviceIdentifier,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
