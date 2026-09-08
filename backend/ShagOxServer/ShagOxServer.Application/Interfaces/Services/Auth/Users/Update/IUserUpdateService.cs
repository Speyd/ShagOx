using ShagOxServer.Application.DTOs.Auth.Users.Update;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Update;
public interface IUserUpdateService
    : IUpdateService<UpdateResponse, UserUpdateRequest>
{
}