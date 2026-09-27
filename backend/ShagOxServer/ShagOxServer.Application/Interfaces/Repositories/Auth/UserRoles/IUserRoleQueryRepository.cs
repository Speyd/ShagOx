using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.UserRoles;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;
public interface IUserRoleQueryRepository
    : IQueryRepository<UserRole, UserRoleSearchFilter>
{
    Task<PagedResult<Role>> GetRolesByUserIdAsync(
        long userId,
        PaginationParams pagination);

    Task<PagedResult<User>> GetUsersByRoleIdAsync(
        long roleId,
        PaginationParams pagination);
}