using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Users;
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
        return await _db.Users
            .WithIncludes()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<User>> GetPagedAsync(
        PaginationParams pagination)
    {
        return await _db.Users
            .WithIncludes()
            .WithPagination(pagination)
            .ToListAsync();
    }

    public async Task<User?> GetByContactAsync(string? email, string? phone)
    {
        var query = _db.Users
            .WithIncludes();

        if (!string.IsNullOrWhiteSpace(email))
            query = query.Where(x => x.Email == email);

        if (!string.IsNullOrWhiteSpace(phone))
            query = query.Where(x => x.Phone == phone);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _db.Users
            .WithIncludes()
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetByPhoneAsync(string phone)
    {
        return await _db.Users
            .WithIncludes()
            .FirstOrDefaultAsync(x => x.Phone == phone);
    }

    public async Task<List<User>> GetByCityAsync(
        int cityId,
        PaginationParams pagination)
    {
        return await _db.Users
            .WithIncludes()
            .Where(x => x.CityId == cityId)
            .WithPagination(pagination)
            .ToListAsync();
    }

    public async Task<List<User>> GetUsersRegisteredAfterAsync(
        DateTime date,
        PaginationParams pagination)
    {
        var dayStart = date.Date;
        var dayEnd = dayStart.AddDays(1);

        return await _db.Users
            .WithIncludes()
            .Where(x => x.RegisteredAt >= dayStart && x.RegisteredAt < dayEnd)
            .WithPagination(pagination)
            .ToListAsync();
    }

    public async Task<List<User>> GetUsersActiveAfterAsync(
        DateTime date,
        PaginationParams pagination)
    {
        var dayStart = date.Date;
        var dayEnd = dayStart.AddDays(1);

        return await _db.Users
            .WithIncludes()
            .Where(x => x.LastSeenAt >= dayStart && x.LastSeenAt < dayEnd)
            .WithPagination(pagination)
            .ToListAsync();
    }

    public async Task<List<User>> Search(
        UserSearchFilter filter, 
        PaginationParams pagination)
    {
        return await _db.Users
            .WithIncludes()
             .Filter(filter)
             .WithPagination(pagination)
             .ToListAsync();
    }
}