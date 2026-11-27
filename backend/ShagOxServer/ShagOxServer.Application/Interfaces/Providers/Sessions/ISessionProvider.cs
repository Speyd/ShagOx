using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.Interfaces.Providers.User;
public interface ISessionProvider
{
    Task<UserSession?> GetSessionAsync(
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}
