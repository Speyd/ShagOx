using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Roles;
public class RoleExistsRepository
    : ExistsRepository<Role>,
      IRoleExistsRepository
{
    public RoleExistsRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _db.Roles
            .AnyAsync(r => r.Name == name);
    }
}