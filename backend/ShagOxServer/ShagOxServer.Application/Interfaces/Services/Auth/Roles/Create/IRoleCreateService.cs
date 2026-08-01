using ShagOxServer.Application.DTOs.Auth.Roles.Create;
using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Roles.Create;
public interface IRoleCreateService
{
    Task<Result<CreateResponse>> CreateAsync(
        RoleCreateRequest request);
}