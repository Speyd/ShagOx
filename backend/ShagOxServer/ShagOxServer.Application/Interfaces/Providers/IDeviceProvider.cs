using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.Interfaces.Providers;
public interface IDeviceProvider
{
    Task<UserDevice?> GetDeviceAsync(
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}
