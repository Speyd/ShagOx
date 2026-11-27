using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Special;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
public interface IUserDeviceExistsRepository
    : IExistsRepository<UserDevice>,
    IExistsOwnerRepository
{
    Task<bool> IsBlockedAsync(
        long userId,
        long deviceId,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(
        long userId,
        long deviceId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
