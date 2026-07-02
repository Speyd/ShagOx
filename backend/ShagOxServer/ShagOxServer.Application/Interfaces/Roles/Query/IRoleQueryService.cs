using ShagOxServer.Application.DTOs.Roles;
using ShagOxServer.SharedKernel.Results;

namespace ShagOxServer.Application.Interfaces.Roles.Query;
public interface IRoleQueryService
{
    Task<Result<RoleDto>> GetByIdAsync(int id);

    Task<Result<RoleDto>> GetByNameAsync(string name);

    Task<Result<List<RoleDto>>> SearchByName(
       string name,
       int page,
       int pageSize);
}
