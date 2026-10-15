using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users;
public class UserExistsRepository
    : ExistsRepository<User>,
      IUserExistsRepository
{
    public UserExistsRepository(
        AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsAsync(
        string? email,
        string? phone,
        string userName,
        CancellationToken cancellationToken = default)
    {
        return await _db.Users.AnyAsync(x =>
            (email != null && x.Email == email) ||
            (phone != null && x.Phone == phone) ||
            x.UserName == userName,
            cancellationToken
        );
    }

    public async Task<bool> ExistsAsync(
        string value,
        CancellationToken cancellationToken = default)
    {
        return await _db.Users.AnyAsync(x =>
            x.Email == value ||
            x.Phone == value ||
            x.UserName == value,
            cancellationToken
        );
    }

    public async Task<bool> ExistsByUserNameAsync(
        string userName,
        CancellationToken cancellationToken = default)
    {
        return await _db.Users
            .AnyAsync(x => 
                x.UserName == userName,
                cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(
        string? email,
        CancellationToken cancellationToken = default)
    {
        return await _db.Users.AnyAsync(x =>
            email != null && x.Email != null &&
            x.Email.ToLower() == email.ToLower()
        );
    }

    public async Task<bool> ExistsByPhoneAsync(
        string? phone,
        CancellationToken cancellationToken = default)
    {
        return await _db.Users.AnyAsync(x =>
            (phone != null && x.Phone == phone),
            cancellationToken
        );
    }
}
