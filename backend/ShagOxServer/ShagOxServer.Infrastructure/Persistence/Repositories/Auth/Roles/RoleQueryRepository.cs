using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces.Auth.Roles;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Roles;
public class RoleQueryRepository : BaseRepository, IRoleQueryRepository
{
    public RoleQueryRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<Role?> GetByNameAsync(string name)
    {
        return await _db.Roles
            .FirstOrDefaultAsync(x => x.Name == name);
    }

}
