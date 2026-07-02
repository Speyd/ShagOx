using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Infrastructure.Interfaces.Auth.Roles;
public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(int id);

    Task AddAsync(Role role);

    Task<bool> UpdateAsync(Role role);

    Task DeleteAsync(Role role);
}
