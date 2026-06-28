using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces.Auth;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth;

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(AppDbContext db)
        :base(db)
    {}

    public IQueryable<User> Query()
    {
        return _db.Users
            .Include(x => x.City)
            .Include(x => x.UserRoles)
                .ThenInclude(r => r.Role);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await Query()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> GetByContactAsync(string? email, string? phone)
    {
        var query = Query();

        if (!string.IsNullOrWhiteSpace(email))
            query = query.Where(x => x.Email == email);

        if (!string.IsNullOrWhiteSpace(phone))
            query = query.Where(x => x.Phone == phone);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await Query()
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetByPhoneAsync(string phone)
    {
        return await Query()
            .FirstOrDefaultAsync(x => x.Phone == phone);
    }

    public async Task<List<User>> GetByCityAsync(int cityId)
    {
        return await Query()
            .Where(x => x.CityId == cityId)
            .ToListAsync();
    }

    public async Task<List<User>> GetUsersRegisteredAfterAsync(DateTime date)
    {
        var dayStart = date.Date;              
        var dayEnd = dayStart.AddDays(1); 

        return await Query()
            .Where(x => x.RegisteredAt >= dayStart && x.RegisteredAt < dayEnd)
            .ToListAsync();
    }

    public async Task<List<User>> GetUsersActiveAfterAsync(DateTime date)
    {
        var dayStart = date.Date;
        var dayEnd = dayStart.AddDays(1);

        return await Query()
            .Where(x => x.LastSeenAt >= dayStart && x.LastSeenAt < dayEnd)
            .ToListAsync();
    }


    public async Task<bool> ExistsAsync(string? email, string? phone)
    {
        return await _db.Users.AnyAsync(x =>
            (email != null && x.Email == email) ||
            (phone != null && x.Phone == phone)
        );
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _db.Users.AnyAsync(x => x.Id == id);
    }

    public async Task<bool> ExistsEmailAsync(string? email)
    {
        return await _db.Users.AnyAsync(x =>
            (email != null && x.Email == email)
        );
    }

    public async Task<bool> ExistsPhoneAsync(string? phone)
    {
        return await _db.Users.AnyAsync(x =>
            (phone != null && x.Phone == phone)
        );
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