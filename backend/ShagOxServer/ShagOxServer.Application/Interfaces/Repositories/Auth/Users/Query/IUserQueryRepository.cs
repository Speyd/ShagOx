using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
public partial interface IUserQueryRepository
    : ISearchRepository<User, UserSearchFilter>
{
    Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default);

    Task<User?> GetByPhoneAsync(
        string phone,
        CancellationToken cancellationToken = default);

    Task<User?> GetByUserNameAsync(
        string userName,
        CancellationToken cancellationToken = default);

    Task<User?> GetByContactAsync(
        string value,
        CancellationToken cancellationToken = default);

    Task<User?> GetByContactAsync(
        string? email,
        string? phone,
        string? userName = null,
        CancellationToken cancellationToken = default);

    Task<PagedResult<User>> AdminSearch(
        UserAdminSearchFilter filter,
        PaginationParams pagination,
        CancellationToken cancellationToken = default);
}
