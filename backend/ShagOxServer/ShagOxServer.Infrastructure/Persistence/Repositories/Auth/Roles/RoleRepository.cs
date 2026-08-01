using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Roles;
public class RoleRepository 
    : BaseRepository, IRoleRepository
{
    public RoleRepository(AppDbContext db)
        :base(db)
    {}
  

    public async Task<Role?> GetByIdAsync(int id)
    {
        return await _db.Roles
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Add(Role role)
    {
        _db.Roles.Add(role);
    }

    public void Delete(Role role)
    {
        _db.Roles.Remove(role);
    }

    public bool Update(Role role)
    {   
        _db.Roles.Update(role);
        return true;
    }
}