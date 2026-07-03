using ShagOxServer.Application.DTOs.Roles;
using ShagOxServer.Domain.Filters.Roles;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Roles.Query;
public interface IRoleQueryService
{
    Task<Result<RoleDto>> GetByIdAsync(int id);

    Task<Result<List<RoleDto>>> GetByUserIdAsync(
       int userId,
       PaginationParams pagination);

    Task<Result<RoleDto>> GetByNameAsync(string name);

    Task<Result<List<RoleDto>>> Search(
       RoleSearchFilter filter,
       PaginationParams pagination);
}
