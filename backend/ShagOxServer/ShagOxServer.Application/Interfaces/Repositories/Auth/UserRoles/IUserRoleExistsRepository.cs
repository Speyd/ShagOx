using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;
public interface IUserRoleExistsRepository 
    : IExistsRepository<UserRole>
{
    Task<bool> ExistsAsync(int roleId, int userId);
}