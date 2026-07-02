using ShagOxServer.Application.DTOs.Roles;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Roles.Query;
public interface IRoleQueryService
{
    Task<Result<RoleDto>> GetByIdAsync(int id);

    Task<Result<RoleDto>> GetByNameAsync(string name);

    Task<Result<List<RoleDto>>> SearchByName(
       string name,
       PaginationParams pagination);
}
