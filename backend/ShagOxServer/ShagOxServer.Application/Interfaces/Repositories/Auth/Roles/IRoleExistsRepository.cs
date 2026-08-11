using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
public interface IRoleExistsRepository 
    : IExistsRepository<Role>
{
    Task<bool> ExistsByNameAsync(string name);
}