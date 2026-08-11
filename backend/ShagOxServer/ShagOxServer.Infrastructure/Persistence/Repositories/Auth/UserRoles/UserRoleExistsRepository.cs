using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles;
public class UserRoleExistsRepository 
    : ExistsRepository<UserRole>, 
      IUserRoleExistsRepository
{
    public UserRoleExistsRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsAsync(
        int roleId, 
        int userId)
    {
        return await _db.UserRoles
            .AnyAsync(x => x.UserId == userId && x.RoleId == roleId);
    }
}