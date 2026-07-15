using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;
public interface IUserRoleQueryRepository
{
    Task<List<Role>> GetRolesByUserIdAsync(
        int userId,
        PaginationParams pagination);

    Task<List<User>> GetUsersByRoleIdAsync(
        int roleId,
        PaginationParams pagination);
}