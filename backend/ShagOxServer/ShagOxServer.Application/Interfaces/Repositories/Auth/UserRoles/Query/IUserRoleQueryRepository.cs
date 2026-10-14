using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.UserRoles;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles.Query;
public partial interface IUserRoleQueryRepository
    : ISearchRepository<UserRole, UserRoleSearchFilter>
{
    Task<PagedResult<Role>> GetRolesByUserAsync(
        long userId,
        PaginationParams pagination);

    Task<PagedResult<User>> GetUsersByRoleAsync(
        long roleId,
        PaginationParams pagination);
}
