using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces.Auth;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth;

public class RoleRepository : BaseRepository, IRoleRepository
{
    public RoleRepository(AppDbContext db)
        :base(db)
    {}

    public async Task<Role?> GetByNameAsync(string name)
    {
        return await _db.Roles
            .FirstOrDefaultAsync(x => x.Name == name);
    }
}
