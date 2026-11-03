using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Delete;
public interface IUserDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<DeleteResponse>
{
}
