using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
public interface IUserQueryRepository
    : IQueryRepository<User, UserSearchFilter>
{
    Task<User?> GetByEmailAsync(
        string email);

    Task<User?> GetByPhoneAsync(
        string phone);

    Task<User?> GetByUserNameAsync(
        string userName);

    Task<User?> GetByContactAsync(
        string value);

    Task<User?> GetByContactAsync(
        string? email,
        string? phone,
        string? userName = null);

    Task<PagedResult<User>> AdminSearch(
        UserAdminSearchFilter filter,
        PaginationParams pagination);
}