using ShagOxServer.Application.DTOs.Advertisements.Favorites;
using ShagOxServer.Application.DTOs.Auth.Roles;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Filters.Roles;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Roles.Query;
public interface IRoleQueryService
    : IQueryService<RoleDto>
{
    Task<Result<PagedResult<RoleDto>>> GetByUserAsync(
       int userId,
       PaginationParams pagination);

    Task<Result<RoleDto>> GetByNameAsync(string name);

    Task<Result<PagedResult<RoleDto>>> Search(
       RoleSearchFilter filter,
       PaginationParams pagination);
}