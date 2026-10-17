using ShagOxServer.Application.DTOs.Auth.Roles.Create;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Create;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Roles.Create;
public interface IRoleCreateService
    : ICreateService<
        CreateResponse,
        RoleCreateRequest
        >
{
}