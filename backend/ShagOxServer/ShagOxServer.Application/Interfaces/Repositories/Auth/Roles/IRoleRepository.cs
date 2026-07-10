using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(int id);

    void Add(Role role);

    bool Update(Role role);

    void Delete(Role role);
}
