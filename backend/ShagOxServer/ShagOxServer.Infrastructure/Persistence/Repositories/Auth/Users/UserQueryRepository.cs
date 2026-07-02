using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces.Auth.Users;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users;
public class UserQueryRepository : BaseRepository, IUserQueryRepository
{
    public UserQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<User?> GetByIdAsync(int id)
    {
        return await _db.Users.WithIncludes()
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<User?> GetByContactAsync(string? email, string? phone)
    {
        var query = _db.Users.WithIncludes();

        if (!string.IsNullOrWhiteSpace(email))
            query = query.Where(x => x.Email == email);

        if (!string.IsNullOrWhiteSpace(phone))
            query = query.Where(x => x.Phone == phone);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _db.Users.WithIncludes()
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetByPhoneAsync(string phone)
    {
        return await _db.Users.WithIncludes()
            .FirstOrDefaultAsync(x => x.Phone == phone);
    }

    public async Task<List<User>> GetByCityAsync(int cityId)
    {
        return await _db.Users.WithIncludes()
            .Where(x => x.CityId == cityId)
            .ToListAsync();
    }

    public async Task<List<User>> GetUsersRegisteredAfterAsync(DateTime date)
    {
        var dayStart = date.Date;
        var dayEnd = dayStart.AddDays(1);

        return await _db.Users.WithIncludes()
            .Where(x => x.RegisteredAt >= dayStart && x.RegisteredAt < dayEnd)
            .ToListAsync();
    }

    public async Task<List<User>> GetUsersActiveAfterAsync(DateTime date)
    {
        var dayStart = date.Date;
        var dayEnd = dayStart.AddDays(1);

        return await _db.Users.WithIncludes()
            .Where(x => x.LastSeenAt >= dayStart && x.LastSeenAt < dayEnd)
            .ToListAsync();
    }

    public async Task<List<User>> SearchByFullName(
        string fullName, 
        PaginationParams pagination)
    {
        return await _db.Users.WithIncludes()
             .Where(x =>
                (x.Name + " " + x.Surname).Contains(fullName))
             .Skip((pagination.Page - 1) * pagination.PageSize)
             .Take(pagination.PageSize)
             .ToListAsync();
    }

    public async Task<List<User>> SearchByEmail(
        string email,
        PaginationParams pagination)
    {
        return await _db.Users.WithIncludes()
             .Where(x =>
                (x.Email != null && x.Email.Contains(email)))
             .Skip((pagination.Page - 1) * pagination.PageSize)
             .Take(pagination.PageSize)
             .ToListAsync();
    }

    public async Task<List<User>> SearchByPhone(
        string phone,
        PaginationParams pagination)
    {
        return await _db.Users.WithIncludes()
            .Where(x =>
               (x.Phone != null && x.Phone.Contains(phone)))
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }
}
