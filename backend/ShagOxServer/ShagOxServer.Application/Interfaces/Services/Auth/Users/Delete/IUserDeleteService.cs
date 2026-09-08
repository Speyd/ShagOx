using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Delete;
public interface IUserDeleteService
    : IDeleteService<DeleteResponse>
{
}