using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
public partial interface IUserQueryRepository
    : ISearchRepository<User, UserSearchFilter>
{
    Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<User?> GetByPhoneAsync(
        string phone,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<User?> GetByUserNameAsync(
        string userName,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<User?> GetByContactAsync(
        string value,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<User?> GetByContactAsync(
        string? email,
        string? phone,
        string? userName = null,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<PagedResult<User>> AdminSearch(
        UserAdminSearchFilter filter,
        PaginationParams pagination,
        CancellationToken cancellationToken = default);
}
