using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(int id);

    Task AddAsync(Role role);

    Task<bool> UpdateAsync(Role role);

    Task DeleteAsync(Role role);
}
