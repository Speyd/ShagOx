using ShagOxServer.Application.DTOs.Roles.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Roles.Update;
public interface IRoleUpdateService
{
    Task<Result<RoleUpdateResponse>> UpdateAsync(
        int roleId,
        RoleUpdateRequest request);
}