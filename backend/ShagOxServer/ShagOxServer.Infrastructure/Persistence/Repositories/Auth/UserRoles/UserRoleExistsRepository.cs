using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles;
public class UserRoleExistsRepository 
    : RepositoryContext, IUserRoleExistsRepository
{
    public UserRoleExistsRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsAsync(int roleId, int userId)
    {
        return await _db.UserRoles
            .AnyAsync(x => x.UserId == userId && x.RoleId == roleId);
    }
}