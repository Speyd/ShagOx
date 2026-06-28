using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Infrastructure.Interfaces.Auth;
public interface IUserRoleRepository
{
    Task<bool> ExistsAsync(int roleId, int userId);

    Task<List<Role>> GetRolesByUserIdAsync(int userId);

    Task<List<User>> GetUsersByRoleIdAsync(
        int roleId,
        int page = 1,
        int pageSize = 20);
}
