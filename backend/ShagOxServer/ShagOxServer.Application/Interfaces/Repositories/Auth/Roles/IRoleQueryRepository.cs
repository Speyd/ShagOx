using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Roles;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
public interface IRoleQueryRepository
{
    Task<Role?> GetByIdAsync(int id);

    Task<List<Role>> GetPagedAsync(
        PaginationParams pagination);

    Task<List<Role>> GetByUserAsync(
        int userId,
        PaginationParams pagination);

    Task<Role?> GetByNameAsync(string name);

    Task<List<Role>> Search(
        RoleSearchFilter filter,
        PaginationParams pagination);
}