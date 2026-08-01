using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users;
public class UserRepository 
    : BaseRepository, IUserRepository
{
    public UserRepository(AppDbContext db)
        :base(db)
    {}


    public async Task<User?> GetByIdAsync(int id)
    {
        return await _db.Users
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Add(User user)
    {
        _db.Users.Add(user);
    }


    public bool Update(User user)
    {
        _db.Users.Update(user);
        return true;
    }

    public void Delete(User user)
    {
        _db.Users.Remove(user);
    }
}