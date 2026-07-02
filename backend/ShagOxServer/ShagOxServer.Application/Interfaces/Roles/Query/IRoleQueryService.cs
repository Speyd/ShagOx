using ShagOxServer.Application.DTOs.Roles;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Paginations;

namespace ShagOxServer.Application.Interfaces.Roles.Query;
public interface IRoleQueryService
{
    Task<Result<RoleDto>> GetByIdAsync(int id);

    Task<Result<RoleDto>> GetByNameAsync(string name);

    Task<Result<List<RoleDto>>> SearchByName(
       string name,
       PaginationParams pagination);
}
