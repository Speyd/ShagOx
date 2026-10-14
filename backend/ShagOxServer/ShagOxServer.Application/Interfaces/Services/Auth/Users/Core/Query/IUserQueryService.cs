using ShagOxServer.Application.DTOs.Auth.Roles.Query;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
public interface IUserQueryService
    : IQueryService<UserDto, User, UserSearchFilter>
{
    Task<Result<UserDto>> GetMyProfileAsync(
        CancellationToken cancellationToken = default);

    Task<Result<UserShortDto>> GetByContactAsync(
        string value,
        CancellationToken cancellationToken = default);

    Task<Result<PagedResult<RoleDto>>> GetMyRoleAsync(
        PaginationParams pagination,
        CancellationToken cancellationToken = default);
}
