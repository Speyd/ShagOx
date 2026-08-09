using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Roles;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
public interface IRoleQueryRepository
    : IQueryRepository<Role>
{
    Task<PagedResult<Role>> GetByUserAsync(
        int userId,
        PaginationParams pagination);

    Task<Role?> GetByNameAsync(string name);

    Task<PagedResult<Role>> Search(
        RoleSearchFilter filter,
        PaginationParams pagination);
}