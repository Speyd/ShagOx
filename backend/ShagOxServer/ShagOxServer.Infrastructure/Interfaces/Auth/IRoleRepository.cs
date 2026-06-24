using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Infrastructure.Interfaces.Auth;
public interface IRoleRepository
{
    Task<Role?> GetByNameAsync(string name);
}
