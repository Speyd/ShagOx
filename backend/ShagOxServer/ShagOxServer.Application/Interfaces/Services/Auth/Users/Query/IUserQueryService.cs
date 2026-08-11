using ShagOxServer.Application.DTOs.Auth.Roles;
using ShagOxServer.Application.DTOs.Auth.Users;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Filters.Users;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Query;
public interface IUserQueryService
    : IQueryService<UserDto>
{
    Task<Result<UserDto>> GetMyProfileAsync();

    Task<Result<PagedResult<RoleDto>>> GetMyRoleAsync(
        PaginationParams pagination);

    Task<Result<PagedResult<UserDto>>> Search(
        UserSearchFilter filter,
        PaginationParams pagination);
}