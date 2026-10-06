using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles.Query;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.UserRoles;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles.Query;
public partial class UserRoleQueryRepository 
    : QueryRepository<UserRole, UserRoleSearchFilter>, 
      IUserRoleQueryRepository
{
    public UserRoleQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<PagedResult<Role>> GetRolesByUserAsync(
       long userId,
       PaginationParams pagination)
    {
        return await _db.Roles
            .WithUserIncludes()
            .Where(u => u.UserRoles.Any(ur => ur.UserId == userId))
            .ToPagedResultAsync(pagination);
    }

    public async Task<PagedResult<User>> GetUsersByRoleAsync(
        long roleId,
        PaginationParams pagination)
    {
        return await _db.Users
            .WithRoleIncludes()
            .Where(u => u.UserRoles.Any(ur => ur.RoleId == roleId))
            .ToPagedResultAsync(pagination);
    }
}