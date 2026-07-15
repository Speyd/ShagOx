using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles;
public class UserRoleQueryRepository : BaseRepository, IUserRoleQueryRepository
{
    public UserRoleQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<List<Role>> GetRolesByUserIdAsync(
       int userId,
       PaginationParams pagination)
    {
        return await _db.Roles
            .WithUserIncludes()
            .Where(u => u.UserRoles.Any(ur => ur.UserId == userId))
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }

    public async Task<List<User>> GetUsersByRoleIdAsync(
        int roleId,
        PaginationParams pagination)
    {
        return await _db.Users
            .WithRoleIncludes()
            .Where(u => u.UserRoles.Any(ur => ur.RoleId == roleId))
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }
}
