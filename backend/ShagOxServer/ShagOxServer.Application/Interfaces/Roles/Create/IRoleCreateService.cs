using ShagOxServer.SharedKernel.Results;
using ShagOxServer.Application.DTOs.Roles.Create;

namespace ShagOxServer.Application.Interfaces.Roles.Create;
public interface IRoleCreateService
{
    Task<Result<RoleCreateResponse>> CreateRoleAsync(
        RoleCreateRequest request);
}
