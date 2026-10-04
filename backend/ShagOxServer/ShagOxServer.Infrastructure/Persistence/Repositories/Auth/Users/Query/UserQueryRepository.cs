using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.Users;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Query;
public partial class UserQueryRepository 
    : QueryRepository<User, UserSearchFilter>, 
      IUserQueryRepository
{
    public UserQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<User?> GetByContactAsync(
        string? email,
        string? phone,
        string? userName = null)
    {
        var query = _db.Users
            .WithIncludes();

        if (!string.IsNullOrWhiteSpace(email))
            query = query.Where(x => x.Email == email);

        if (!string.IsNullOrWhiteSpace(phone))
            query = query.Where(x => x.Phone == phone);

        if (!string.IsNullOrWhiteSpace(userName))
            query = query.Where(x => x.UserName == userName);


        return await query.FirstOrDefaultAsync();
    }

    public async Task<User?> GetByContactAsync(
       string value)
    {
        return await _db.Users
            .WithIncludes()
            .FirstOrDefaultAsync(x =>
                x.Email == value ||
                x.Phone == value ||
                x.UserName == value
            );
    }

    public async Task<User?> GetByEmailAsync(
        string email)
    {
        return await _db.Users
            .WithIncludes()
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetByPhoneAsync(
        string phone)
    {
        return await _db.Users
            .WithIncludes()
            .FirstOrDefaultAsync(x => x.Phone == phone);
    }

    public async Task<User?> GetByUserNameAsync(
       string userName)
    {
        return await _db.Users
            .WithIncludes()
            .FirstOrDefaultAsync(x => x.UserName == userName);
    }

    public async Task<PagedResult<User>> AdminSearch(
        UserAdminSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.Users
            .WithIncludes()
            .Filter(filter)
            .ToPagedResultAsync(pagination);
    }
}