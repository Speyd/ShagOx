using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces.Auth;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

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
        PaginationParams pagination)
    {
        return await _db.Roles
            .Where(u => u.UserRoles.Any(ur => ur.UserId == userId))
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.User)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }

    public async Task<List<User>> GetUsersByRoleIdAsync(
        int roleId,
        PaginationParams pagination)
    {
        return await _db.Users
            .Where(u => u.UserRoles.Any(ur => ur.RoleId == roleId))
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }
}
