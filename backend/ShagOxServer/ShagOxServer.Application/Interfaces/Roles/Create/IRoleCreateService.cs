using ShagOxServer.Application.DTOs.Roles.Create;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Roles.Create;
public interface IRoleCreateService
{
    Task<Result<RoleCreateResponse>> CreateRoleAsync(
        RoleCreateRequest request);
}
