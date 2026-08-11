using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users;
public class UserExistsRepository
    : ExistsRepository<User>,
      IUserExistsRepository
{
    public UserExistsRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsAsync(
        string? email,
        string? phone)
    {
        return await _db.Users.AnyAsync(x =>
            (email != null && x.Email == email) ||
            (phone != null && x.Phone == phone)
        );
    }

    public async Task<bool> ExistsEmailAsync(
        string? email)
    {
        return await _db.Users.AnyAsync(x =>
            (email != null && x.Email == email)
        );
    }

    public async Task<bool> ExistsPhoneAsync(
        string? phone)
    {
        return await _db.Users.AnyAsync(x =>
            (phone != null && x.Phone == phone)
        );
    }
}