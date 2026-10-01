using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.UserRoles;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;
public interface IUserRoleQueryRepository
    : IQueryRepository<UserRole, UserRoleSearchFilter>
{
    Task<PagedResult<Role>> GetRolesByUserAsync(
        long userId,
        PaginationParams pagination);

    Task<List<long>> GetRoleIdsByUserAsync(
        long userId);

    Task<PagedResult<User>> GetUsersByRoleAsync(
        long roleId,
        PaginationParams pagination);

    Task<List<long>> GetUserIdsByRoleAsync(
        long roleId);
}