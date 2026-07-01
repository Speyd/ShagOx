using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces.Auth.Users;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users;

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(AppDbContext db)
        :base(db)
    {}


    public async Task<User?> GetByIdAsync(int id)
    {
        return await _db.Users
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(User user)
    {
        await _db.Users.AddAsync(user);

        await _db.SaveChangesAsync();
    }


    public async Task<bool> UpdateAsync(User user)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task DeleteAsync(User user)
    {
        _db.Users.Remove(user);

        await _db.SaveChangesAsync();
    }
}