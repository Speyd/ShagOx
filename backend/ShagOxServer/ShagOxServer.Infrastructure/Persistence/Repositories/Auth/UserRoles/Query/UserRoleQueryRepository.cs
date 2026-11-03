using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.UserRoles;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles.Query;
public partial class UserRoleQueryRepository 
    : SearchRepository<UserRole, UserRoleSearchFilter>, 
      IUserRoleQueryRepository
{
    public UserRoleQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<PagedResult<Role>> GetRolesByUserAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Roles
            .WithUserIncludes()
            .WithStatusFilter(status)
            .Where(u => u.UserRoles
                .Any(ur => ur.UserId == userId))
            .ToPagedResultAsync(pagination,
                (x => x.Id),
                cancellationToken);
    }

    public async Task<PagedResult<User>> GetUsersByRoleAsync(
        long roleId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Users
            .WithRoleIncludes()
            .WithStatusFilter(status)
            .Where(u => u.UserRoles.Any(ur => ur.RoleId == roleId))
            .ToPagedResultAsync(pagination,
                (x => x.Id),
                cancellationToken);
    }
}
