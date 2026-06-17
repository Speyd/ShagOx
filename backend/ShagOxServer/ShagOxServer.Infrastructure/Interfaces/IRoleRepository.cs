using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Infrastructure.Interfaces;
public interface IRoleRepository
{
    Task<Role?> GetByNameAsync(string name);
}
