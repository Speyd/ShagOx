using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.Roles;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Roles.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Roles;
public class RoleQueryRepository 
    : QueryRepository<Role, RoleSearchFilter>,
      IRoleQueryRepository
{
    public RoleQueryRepository(AppDbContext db)
        : base(db)
    { }


    protected override IQueryable<Role> ApplyFilter(
      IQueryable<Role> query,
      RoleSearchFilter filter)
    {
        return query.Filter(filter);
    }

    public async Task<PagedResult<Role>> GetByUserAsync(
       long userId,
       PaginationParams pagination)
    {
        return await _db.UserRoles
            .Where(x => x.UserId == userId)
            .Select(x => x.Role)
            .ToPagedResultAsync(pagination);
    }

    public async Task<Role?> GetByNameAsync(
        string name)
    {
        return await _db.Roles
            .FirstOrDefaultAsync(x => x.Name == name);
    }
}