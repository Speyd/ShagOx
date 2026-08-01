using ShagOxServer.Application.DTOs.Auth.Users;
using ShagOxServer.Domain.Filters.Users;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Query;
public interface IUserAdminQueryService
{
    Task<Result<PagedResult<UserDto>>> GetPagedAsync(
        PaginationParams pagination);

    Task<Result<PagedResult<UserDto>>> Search(
        UserAdminSearchFilter filter,
        PaginationParams pagination);

    Task<bool> ExistsByIdAsync(int id);
}