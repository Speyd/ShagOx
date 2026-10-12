using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.Roles;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
public interface IRoleQueryRepository
    : ISearchRepository<Role, RoleSearchFilter>
{
    Task<PagedResult<Role>> GetByUserAsync(
        long userId,
        PaginationParams pagination);

    Task<Role?> GetByNameAsync(
        string name);
}
