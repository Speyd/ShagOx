using ShagOxServer.Application.DTOs.Auth.Roles.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.Application.DTOs.Common.Responses;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Roles.Update;
public interface IRoleUpdateService
{
    Task<Result<UpdateResponse>> UpdateAsync(
        int roleId,
        RoleUpdateRequest request);
}