using ShagOxServer.Application.Interfaces.Repositories.Devices.Query;
using ShagOxServer.Domain.Entities.Devices;
using ShagOxServer.Domain.Filters.Devices;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Devices.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Devices.Query;
public partial class DeviceQueryRepository
    : SearchRepository<Device, DeviceSearchFilter>,
      IDeviceQueryRepository
{
    public override IQueryable<Device> ApplyFilter(
        IQueryable<Device> query,
        DeviceSearchFilter filter)
    {
        return query.Filter(filter);
    }
}
