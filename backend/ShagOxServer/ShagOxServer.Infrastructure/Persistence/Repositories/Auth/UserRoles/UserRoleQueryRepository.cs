using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Auth.Roles.Cache;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Cache;
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

    public async Task<List<RoleCacheInfo>> GetRoleCacheInfoByUserAsync(
        long userId)
    {
        return await _db.Roles
            .Where(u => u.UserRoles
                .Any(ur => ur.UserId == userId))
            .SelectRoleCacheInfo()
            .ToListAsync();
    }

    public async Task<List<UserCacheInfo>> GetUserCacheInfoByRoleAsync(
        long roleId)
    {
        return await _db.Users
            .Where(u => u.UserRoles
                .Any(ur => ur.RoleId == roleId))
            .SelectUserCacheInfo()
            .ToListAsync();
    }
}