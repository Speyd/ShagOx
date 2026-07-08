using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Roles;
public class RoleExistsRepository : BaseRepository, IRoleExistsRepository
{
    public RoleExistsRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _db.Roles.AnyAsync(r => r.Id == id);
    }

    public async Task<bool> ExistsAsync(string name)
    {
        return await _db.Roles.AnyAsync(r => r.Name == name);
    }
}
