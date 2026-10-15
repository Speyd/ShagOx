using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.Users;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Query;
public partial class UserQueryRepository 
    : SearchRepository<User, UserSearchFilter>, 
      IUserQueryRepository
{
    public UserQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<User?> GetByContactAsync(
        string? email,
        string? phone,
        string? userName = null,
        CancellationToken cancellationToken = default)
    {
        var query = _db.Users
            .WithIncludes();

        if (!string.IsNullOrWhiteSpace(email))
            query = query.Where(x => x.Email == email);

        if (!string.IsNullOrWhiteSpace(phone))
            query = query.Where(x => x.Phone == phone);

        if (!string.IsNullOrWhiteSpace(userName))
            query = query.Where(x => x.UserName == userName);


        return await query
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<User?> GetByContactAsync(
       string value,
        CancellationToken cancellationToken = default)
    {
        return await _db.Users
            .WithIncludes()
            .FirstOrDefaultAsync(x =>
                x.Email == value ||
                x.Phone == value ||
                x.UserName == value,
                cancellationToken
            );
    }

    public async Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        return await _db.Users
            .WithIncludes()
            .FirstOrDefaultAsync(x => 
                x.Email == email,
                cancellationToken);
    }

    public async Task<User?> GetByPhoneAsync(
        string phone,
        CancellationToken cancellationToken = default)
    {
        return await _db.Users
            .WithIncludes()
            .FirstOrDefaultAsync(x => 
                x.Phone == phone,
                cancellationToken);
    }

    public async Task<User?> GetByUserNameAsync(
       string userName,
        CancellationToken cancellationToken = default)
    {
        return await _db.Users
            .WithIncludes()
            .FirstOrDefaultAsync(x => 
                x.UserName == userName,
                cancellationToken);
    }

    public async Task<PagedResult<User>> AdminSearch(
        UserAdminSearchFilter filter,
        PaginationParams pagination,
        CancellationToken cancellationToken = default)
    {
        return await _db.Users
            .WithIncludes()
            .Filter(filter)
            .ToPagedResultAsync(pagination,
                (x => x.Id),
                cancellationToken);
    }
}
