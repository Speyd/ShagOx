using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Roles;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Roles.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Roles;
public class RoleQueryRepository : BaseRepository, IRoleQueryRepository
{
    public RoleQueryRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<Role?> GetByIdAsync(int id)
    {
        return await _db.Roles
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Role>> GetByUserIdAsync(
       int userId,
       PaginationParams pagination)
    {
        return await _db.UserRoles
            .Where(x => x.UserId == userId)
            .Select(x => x.Role)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        return await _db.Roles
            .FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<List<Role>> Search(
        RoleSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.Roles
            .Filter(filter)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }
}
