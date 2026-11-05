using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Roles.Delete;
public interface IRoleDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<Role, DeleteResponse>
{
}
