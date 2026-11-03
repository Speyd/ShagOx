using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.Roles;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Roles.Query;
public partial class RoleQueryRepository 
    : SearchRepository<Role, RoleSearchFilter>,
      IRoleQueryRepository
{
    public RoleQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<PagedResult<Role>> GetByUserAsync(
       long userId,
       PaginationParams pagination,
       CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.UserRoles
            .WithStatusFilter(status)
            .Where(x => x.UserId == userId)
            .Select(x => x.Role)
            .ToPagedResultAsync(pagination,
                (x => x.Id),
                cancellationToken);
    }

    public async Task<Role?> GetByNameAsync(
        string name,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Roles
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(x => x.Name == name,
                cancellationToken);
    }
}
