using ShagOxServer.Application.DTOs.Auth.Roles.Query;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.Roles;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Roles.Query;
public interface IRoleQueryService
    : IQueryService<RoleDto, Role, RoleSearchFilter>
{
    Task<Result<PagedResult<RoleDto>>> GetByUserAsync(
       long userId,
       PaginationParams pagination);

    Task<Result<RoleDto>> GetByNameAsync(
        string name);
}