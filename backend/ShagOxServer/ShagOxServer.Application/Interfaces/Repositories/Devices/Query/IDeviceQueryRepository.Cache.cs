using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Entities.Devices;
using ShagOxServer.Domain.Filters.Devices;

namespace ShagOxServer.Application.Interfaces.Repositories.Devices.Query;
public partial interface IDeviceQueryRepository
    : ISearchRepository<Device, DeviceSearchFilter>
{
}
