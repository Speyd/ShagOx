using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Roles.Delete;
public interface IRoleDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<DeleteResponse>
{
}
