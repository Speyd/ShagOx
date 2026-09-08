using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Roles.Delete;
public interface IRoleDeleteService
    : IDeleteService<DeleteResponse>
{
}