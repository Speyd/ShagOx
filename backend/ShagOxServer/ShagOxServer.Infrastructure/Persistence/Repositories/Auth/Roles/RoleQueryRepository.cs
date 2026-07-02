using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces.Auth.Roles;
using ShagOxServer.SharedKernel.Paginations;

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

    public async Task<Role?> GetByNameAsync(string name)
    {
        return await _db.Roles
            .FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<List<Role>> SearchByName(
        string name,
        PaginationParams pagination)
    {
        return await _db.Roles
            .Where(x => x.Name.Contains(name))
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }
}
