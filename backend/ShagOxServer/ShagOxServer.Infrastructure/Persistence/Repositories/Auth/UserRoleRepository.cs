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

    public async Task<List<Role>> GetRolesByUserIdAsync(
        int userId,
        int page = 1,
        int pageSize = 20)
    {
        return await _db.Roles
            .Where(u => u.UserRoles.Any(ur => ur.UserId == userId))
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.User)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<User>> GetUsersByRoleIdAsync(
        int roleId,
        int page = 1,
        int pageSize = 20)
    {
        return await _db.Users
            .Where(u => u.UserRoles.Any(ur => ur.RoleId == roleId))
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}
