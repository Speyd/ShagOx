using ShagOxServer.Application.DTOs.Roles.Create;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Roles.Create;
public interface IRoleCreateService
{
    Task<Result<RoleCreateResponse>> CreateAsync(
        RoleCreateRequest request);
}