using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles;
public class UserRoleExistsRepository 
    : ExistsRepository<UserRole>, 
      IUserRoleExistsRepository
{
    public UserRoleExistsRepository(
        AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsAsync(
        long roleId,
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.UserRoles
            .WithStatusFilter(status)
            .AnyAsync(x => x.UserId == userId &&
                x.RoleId == roleId, cancellationToken);
    }
}
