using ShagOxServer.Application.DTOs.Auth.Roles.Update;
using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Roles.Update;
public interface IRoleUpdateService
    : IUpdateService<UpdateResponse, RoleUpdateRequest>
{
}