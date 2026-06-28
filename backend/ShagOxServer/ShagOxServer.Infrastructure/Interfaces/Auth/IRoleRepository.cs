using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Infrastructure.Interfaces.Auth;
public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(int id);

    Task<Role?> GetByNameAsync(string name);


    Task AddAsync(Role role);

    Task<bool> UpdateAsync(Role role);

    Task DeleteAsync(Role role);
}
