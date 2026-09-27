using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.UserRoles;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles;
public class UserRoleQueryRepository 
    : QueryRepository<UserRole, UserRoleSearchFilter>, 
      IUserRoleQueryRepository
{
    public UserRoleQueryRepository(AppDbContext db)
        : base(db)
    { }


    protected override IQueryable<UserRole> ApplyFilter(
      IQueryable<UserRole> query,
      UserRoleSearchFilter filter)
    {
        return query.Filter(filter);
    }

    public async Task<PagedResult<Role>> GetRolesByUserIdAsync(
       long userId,
       PaginationParams pagination)
    {
        return await _db.Roles
            .WithUserIncludes()
            .Where(u => u.UserRoles.Any(ur => ur.UserId == userId))
            .ToPagedResultAsync(pagination);
    }

    public async Task<PagedResult<User>> GetUsersByRoleIdAsync(
        long roleId,
        PaginationParams pagination)
    {
        return await _db.Users
            .WithRoleIncludes()
            .Where(u => u.UserRoles.Any(ur => ur.RoleId == roleId))
            .ToPagedResultAsync(pagination);
    }
}