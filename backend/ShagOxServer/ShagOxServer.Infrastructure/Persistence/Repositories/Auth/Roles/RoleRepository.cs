using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Roles;

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
