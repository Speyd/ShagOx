using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Roles;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Interfaces.Auth.Roles;
public interface IRoleQueryRepository
{
    Task<Role?> GetByIdAsync(int id);

    Task<List<Role>> GetByUserIdAsync(
        int userId,
        PaginationParams pagination);

    Task<Role?> GetByNameAsync(string name);

    Task<List<Role>> Search(
        RoleSearchFilter filter,
        PaginationParams pagination);
}
