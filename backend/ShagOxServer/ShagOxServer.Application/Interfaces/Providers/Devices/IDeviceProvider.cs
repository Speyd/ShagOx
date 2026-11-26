using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Devices;

namespace ShagOxServer.Application.Interfaces.Providers.Devices;
public interface IDeviceProvider
{
    Task<UserDevice?> GetUserDeviceAsync(
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<Device?> GetDeviceAsync(
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}
