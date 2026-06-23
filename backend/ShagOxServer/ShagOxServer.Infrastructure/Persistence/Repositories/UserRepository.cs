using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces;

namespace ShagOxServer.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(User user)
    {
        await _db.Users.AddAsync(user);

        await _db.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string? email, string? phone)
    {
        return await _db.Users.AnyAsync(x =>
            (email != null && x.Email == email) ||
            (phone != null && x.Phone == phone)
        );
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

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetByPhoneAsync(string phone)
    {
        return await _db.Users.FirstOrDefaultAsync(x => x.Phone == phone);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateAsync(User user)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
        return true;
    }
}