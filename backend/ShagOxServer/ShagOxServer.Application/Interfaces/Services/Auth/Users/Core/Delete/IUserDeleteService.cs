using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Delete;
public interface IUserDeleteService
    : IDeleteService<DeleteResponse>
{
}