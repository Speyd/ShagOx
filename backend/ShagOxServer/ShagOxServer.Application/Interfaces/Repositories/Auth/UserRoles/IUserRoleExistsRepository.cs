using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;
public interface IUserRoleExistsRepository 
    : IExistsRepository<UserRole>
{
    Task<bool> ExistsAsync(
        long roleId,
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
