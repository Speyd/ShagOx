using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces;

namespace ShagOxServer.Infrastructure.Persistence.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _db;
    public RoleRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        return await _db.Roles
            .FirstOrDefaultAsync(x => x.Name == name);
    }
}
