using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Infrastructure.Interfaces.Auth.Roles;
public interface IRoleQueryRepository
{
    Task<Role?> GetByIdAsync(int id);

    Task<Role?> GetByNameAsync(string name);

    Task<List<Role>> SearchByName(
        string name,
        int page,
        int pageSize);
}
