using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Interfaces.Auth;
public interface IUserRoleRepository
{
    Task<bool> ExistsAsync(int roleId, int userId);

    Task<List<Role>> GetRolesByUserIdAsync(
        int userId,
        PaginationParams pagination);

    Task<List<User>> GetUsersByRoleIdAsync(
        int roleId,
        PaginationParams pagination);
}
