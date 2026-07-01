using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Infrastructure.Interfaces.Auth.Roles;
public interface IRoleQueryRepository
{
    Task<Role?> GetByNameAsync(string name);
}
