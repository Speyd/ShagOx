using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Filters.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
public interface IUserAdminQueryService
{
    Task<Result<PagedResult<UserDto>>> GetPagedAsync(
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<Result<PagedResult<UserDto>>> Search(
        UserAdminSearchFilter filter,
        PaginationParams pagination,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByIdAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}
