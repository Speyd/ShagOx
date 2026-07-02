using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Interfaces.Auth.Roles;
public interface IRoleQueryRepository
{
    Task<Role?> GetByIdAsync(int id);

    Task<Role?> GetByNameAsync(string name);

    Task<List<Role>> SearchByName(
        string name,
        PaginationParams pagination);
}
