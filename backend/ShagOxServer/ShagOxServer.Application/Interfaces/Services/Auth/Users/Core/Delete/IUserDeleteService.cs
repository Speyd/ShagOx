using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Auth;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Delete;
public interface IUserDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<User, DeleteResponse>
{
}
