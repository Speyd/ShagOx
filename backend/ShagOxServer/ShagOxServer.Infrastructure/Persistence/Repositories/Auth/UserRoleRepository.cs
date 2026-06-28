using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces.Auth;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth;
public class UserRoleRepository : BaseRepository, IUserRoleRepository
{
    public UserRoleRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsAsync(int roleId, int userId)
    {
        return await _db.UserRoles
            .AnyAsync(x => x.UserId == userId && x.RoleId == roleId);
    }

    public async Task<List<Role>> GetRolesByUserIdAsync(int userId)
    {
        return await _db.Set<UserRole>()
            .Where(x => x.UserId == userId)
            .Include(x => x.Role)
            .Select(x => x.Role!)
            .ToListAsync();
    }

    public async Task<List<User>> GetUsersByRoleIdAsync(
        int roleId,
        int page = 1,
        int pageSize = 20)
    {
        return await _db.Set<UserRole>()
            .Where(x => x.RoleId == roleId)
            .Select(x => x.User!)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}
