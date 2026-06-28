using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces.Auth;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth;

public class RoleRepository : BaseRepository, IRoleRepository
{
    public RoleRepository(AppDbContext db)
        :base(db)
    {}
  
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


    public async Task<bool> ExistsAsync(int id)
    {
        return await _db.Roles.AnyAsync(r => r.Id == id);
    }

    public async Task<bool> ExistsAsync(string name)
    {
        return await _db.Roles.AnyAsync(r => r.Name == name);
    }


    public async Task AddAsync(Role role)
    {
        await _db.Roles.AddAsync(role);

        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Role role)
    {
        _db.Roles.Remove(role);

        await _db.SaveChangesAsync();
    }
    public async Task<bool> UpdateAsync(Role role)
    {   
        _db.Roles.Update(role);
        await _db.SaveChangesAsync();
        return true;
    }
}
