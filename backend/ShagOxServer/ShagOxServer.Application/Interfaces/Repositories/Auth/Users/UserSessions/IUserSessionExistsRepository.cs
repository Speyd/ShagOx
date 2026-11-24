using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Special;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions;
public interface IUserSessionExistsRepository
    : IExistsRepository<UserSession>,
    IExistsOwnerRepository
{
    Task<bool> IsRevokedAsync(
        int userId,
        string refreshToken,
        CancellationToken cancellationToken = default);
}
